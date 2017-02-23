using Pochit.Core.Canvas.Events;
using Pochit.Core.Drawing;
using Pochit.Core.Misc.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Pochit.Core.Canvas
{
    public interface ICanvas
    {
        int Width { get; }
        int Height { get; }

        Task DrawAsync(IAct act);
        IObservable<DrawEventArgs> OnDraw { get; }
        IObservable<IHistory> OnRegisteredHistory { get; }
        Color GetColor(Point point);
        Color GetColor(int x, int y);
        void DrawPoint(int x, int y, Color color);
    }

    class CanvasImpl : ICanvas
    {
        Subject<DrawEventArgs> DrawEventSource { get; } = new Subject<DrawEventArgs>();
        Subject<IHistory> HistorySource { get; } = new Subject<IHistory>();
        Color[,] Data { get; }

        public int Height { get; private set; }
        public int Width { get; private set; }
        public IObservable<DrawEventArgs> OnDraw => DrawEventSource;
        public IObservable<IHistory> OnRegisteredHistory => HistorySource;

        public async Task DrawAsync(IAct act)
        {
            var history = await act.DrawAsync(this);
            DrawEventSource.OnNext(new DrawEventArgs());
            // 描画履歴を返す。
            HistorySource.OnNext(history);
        }

        public Color GetColor(Point point)
        {
            return this.GetColor(point.X, point.Y);
        }

        public Color GetColor(int x, int y)
        {
            return this.Data[x, y];
        }

        public void DrawPoint(int x, int y, Color color)
        {
            this.Data[x, y] = color;
        }
    }

    class HistoryImpl : IHistory
    {
        Action RedoAction { get; }
        Action UndoAction { get; }

        public HistoryImpl(Action redo, Action undo)
        {
            this.UndoAction = undo;
            this.RedoAction = redo;
        }

        public async Task RedoAsync()
        {
            await Task.Run(() => this.RedoAction());
        }

        public async Task UndoAsync()
        {
            await Task.Run(() => this.UndoAction());
        }
    }


}
