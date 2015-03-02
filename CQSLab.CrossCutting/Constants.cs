using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageResizer;

namespace CQSLab.CrossCutting
{
    public static class Constants
    {
        public const int DEFAULT_PAGE_INDEX = 1;
        public const int DEFAULT_PAGE_SIZE = 10;
        public const decimal DEFAULT_BUDGET_VALUE = 0;
        public static readonly ResizeSettings PROFILE_IMAGE_RESIZE_SETTINGS = new ResizeSettings
        {
            Format = "png",
            Scale = ScaleMode.DownscaleOnly,
            MaxWidth = 150,
            MaxHeight = 150,
            Quality = 90,
        };
    }
}
