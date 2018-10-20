using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Dtos
{
    public class DesignDto : AreaDto
    {
        public int Id { get; set; }
    }

    public class AreaDto : SizeDto
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public AreaDto() { }

        public AreaDto(double width, double height) : base(width, height) { }

        public AreaDto(double left, double top, double width, double height)
            : base(width, height)
        {
            Left = left;
            Top = top;
        }
    }

    public class SizeDto
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public SizeDto() { }
        public SizeDto(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
