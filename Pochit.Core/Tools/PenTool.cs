using Pochit.Core.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pochit.Core.Canvas;

namespace Pochit.Core.Tools
{
    public interface ITool
    {
        IReadOnlyList<Tuple<Point, Color>> DrawingPreview(Color color, Point point);
        IAct Draw(Color color, IReadOnlyList<Point> drawingPoints);
    }

    class PenTool : ITool
    {
        public PenTool()
        {
        }

        public IReadOnlyList<Tuple<Point, Color>> DrawingPreview(Color color, Point point)
        {
            return new[] { Tuple.Create(point, color) };
        }

        public IAct Draw(Color color, IReadOnlyList<Point> drawingPoints)
        {
            return new DrawingAct(color, drawingPoints);
        }
    }
}
