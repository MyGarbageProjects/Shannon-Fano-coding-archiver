using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
/// <summary>
///////////////////////////////////////////////////////////////////////    
/////////////////////////////////////////////////////////////////////// 
////////////Change type in method ChangeBorderColor////////////////////
///////////////////Created by student's DGU////////////////////////////
///////////////////////////////////////////////////////////////////////
/// </summary>
/// <Example>
///  Animation<PictureBox> anim = new Animation<PictureBox>();
///  anim.AddElement(pictureBox1);
///  anim.AddElement(pictureBox2);
///  anim.Start(false);
/// </Example>
namespace Animation
{
    class Animation<T>
    {
        Grid grid;
        public Animation(Grid Grid)
        {
            this.grid = Grid;
        }
        List<T> Elements = new List<T>();
        public void AddElement(T element)
        {
            Elements.Add(element);
        }
        public void Start(bool Thread = true)
        {
            if (Thread)
                new Thread(AnimateBackground).Start();
            else
                new Task(AnimateBackground).Start();
        }
        public ushort SpeedAnimation = 50;
        private void AnimateBackground()
        {
            try
            {
                while (true)
                {
                    for (float i = 0; i < 1f; i += 0.01f)
                    {
                        System.Threading.Thread.Sleep(SpeedAnimation); // Торможение потока.
                        ChangeBorderColor(Rainbow(i)); //Вызов функции изменения цвета Бордера
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неизвестная ошибка :{0}", ex.ToString());
            }
        }
        private void ChangeBorderColor(Color cl)
        {
            try
            {
                grid.Dispatcher.Invoke(new Action(() =>
                {
                    for (int i = 0; i < Elements.Count; i++)
                        (Elements[i] as Rectangle/*Change*/).Stroke = new SolidColorBrush(cl);
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private static byte color = 255;
        private static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);//6
            byte ascending = (byte)((div % 1) * 255);//255
            byte descending = (byte)(255 - ascending);//255
            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(color, color, ascending, 0);//255, 255, ascending, 0
                case 1:
                    return Color.FromArgb(color, descending, 255, 0);//255, descending, 255, 0
                case 2:
                    return Color.FromArgb(color, 0, color, ascending);//255, 0, 255, ascending 
                case 3:
                    return Color.FromArgb(color, 0, descending, color);//255, 0, descending, 255 
                case 4:
                    return Color.FromArgb(color, ascending, 0, 255);//255, ascending, 0, 255
                default: // case 5:
                    return Color.FromArgb(color, color, 0, descending);//255, 255, 0, descending
            }
        }
    }
}
