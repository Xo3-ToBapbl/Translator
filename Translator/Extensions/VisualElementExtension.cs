using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Translator.Extensions
{
    public static class VisualElementExtension
    {
        public static (double X, double Y) GetScreenCoordinates(this VisualElement view)
        {
            double screenCoordinateX = view.X;
            double screenCoordinateY = view.Y;

            if (view.Parent.GetType() != typeof(App))
            {
                VisualElement parent = (VisualElement)view.Parent;

                while (parent != null)
                {
                    screenCoordinateX += parent.X;
                    screenCoordinateY += parent.Y;

                    if (parent.Parent.GetType() == typeof(App))
                        parent = null;
                    else
                        parent = (VisualElement)parent.Parent;
                }
            }

            return (screenCoordinateX, screenCoordinateY);
        }
    }
}
