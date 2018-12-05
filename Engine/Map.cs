/**
*   file: Map.cs
	author: Mel and Cole
*/
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
/*
TABLE OF CONTENTS
	* Global
		+ abstract 
		
*/
namespace twoDTDS.Engine
{
    using HA = HorizontalAlignment;
    using VA = VerticalAlignment;
    /*---------------------------------------------------------------------------------------
                                       << MAP >> 
    ---------------------------------------------------------------------------------------*/
    public abstract class Map
    {
        public List<GameObject> Objects = new List<GameObject>();
        public List<GameObject> PaddingObjects = new List<GameObject>();
        public double Width { get; set; }
        public double Height { get; set; }

        /*=========================== Plane >> Acc. =========================*/
        public PlayArea Plane { get; set; }

        /*================================ Map ==============================*/
        public Map(PlayArea Plane)
        {
            this.Plane = Plane;
            Width = Plane.ActualWidth;
            Height = Plane.ActualHeight;
        }

        /*============================= OnRender ============================*/
        public virtual void OnRender(DrawingContext dc)
        {
            foreach (GameObject obj in Objects)
            {
                if (!obj.ObDied)
                {
                    obj.OnRender(dc);
                }
            }
        }

        /*============================= OnUpdate ============================*/
        public virtual void OnUpdate()
        {
            Width = Plane.ActualWidth;
            Height = Plane.ActualHeight;
            ProcessPaddingObjects();

            foreach (GameObject obj in Objects)
            {
                if (!obj.ObDied)
                {
                    obj.OnUpdate();
                }
            }
            ProcessPaddingObjects(true);
        }

        /*======================= ProcessPaddingObjects =====================*/
        internal void ProcessPaddingObjects(bool doUpdate = false)
        {
            if (doUpdate)
            {
                foreach (GameObject obj in PaddingObjects)
                {
                    obj.OnUpdate();
                }
            }
            if (PaddingObjects.Count > 0)
            {
                Objects.AddRange(PaddingObjects);
            }
            PaddingObjects.Clear();
        }

        /*============================= AddObject =========================*/
        public void AddObject(GameObject obj)
        {
            PaddingObjects.Add(obj);
        }

        /*============================= DrwTxt ==============================*/
        public void DrwTxt(DrawingContext dc, string text="", double x = 0, 
                             double y = 0, double size = 16, HA ha = HA.Left, 
                             VA va = VA.Top)
        {
            FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, 
                                   FlowDirection.LeftToRight, Default.Typeface,
                                   size, Brushes.Black);
            double xOffset = 0;
            switch (ha)
            {
                case HA.Center: xOffset = -ft.Width / 2;
                    break;
                case HA.Right: xOffset = -ft.Width;
                    break;
            }
            double yOffset = 0;
            switch (va)
            {
                case VA.Center:
                    yOffset = -ft.Height / 2;
                    break;
                case VA.Bottom:
                    yOffset = -ft.Height;
                    break;
            }

            dc.DrawText(ft, new Point(Math.Round(x + xOffset), 
                        Math.Round(y + yOffset)));
        }

        /*============================= GarbageCollection ===================*/
        internal void GarbageCollection()
        {
            bool on = true;
            int index = 0;
            while (on)
            {
                if (index >= Objects.Count)
                {
                    on = false;
                    break;
                }
                else
                {
                    if (Objects[index].ObDied)
                    {
                        Objects.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }
    }
}
