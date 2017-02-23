using Pochit.Core.Canvas;
using Pochit.Core.Misc.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pochit.Core.Drawing
{
    public interface IAct
    {
        Task<IHistory> DrawAsync(ICanvas canvas);
    }

    public interface IHistory
    {
        Task RedoAsync();
        Task UndoAsync();
    }

    /// <summary>
    /// ピクセル単位の描画操作。
    /// </summary>
    class DrawingAct : IAct
    {
        public DrawingAct(Color color, IReadOnlyList<Point> points)
        {
            this.Points = points.Select(each => Tuple.Create(each, color)).ToList();
        }
        public IReadOnlyList<Tuple<Point, Color>> Points { get; }

        public async Task<IHistory> DrawAsync(ICanvas canvas)
        {
            // 今回の描画イベントの実行結果。
            Action executor = () =>
            {
                this.Points.ForEach(each => canvas.DrawPoint(each.Item1.X, each.Item1.Y, each.Item2));
            };

            // 描画する前の状態を覚えておく。
            var currentColors = this.Points.Select(each => new { X = each.Item1.X, Y = each.Item1.Y, Color = canvas.GetColor(each.Item1.X, each.Item1.Y) }).ToList();

            // 描画実行。
            await Task.Run(() => executor.Invoke());

            // 描画履歴を返す。
            return new HistoryImpl(
                redo: executor,
                undo: () =>
                {
                    currentColors.ForEach(each => canvas.DrawPoint(each.X, each.Y, each.Color));
                });
        }
    }
}
