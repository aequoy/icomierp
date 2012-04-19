using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Expression.Interactivity;
using System.ComponentModel;
using System.Windows.Interactivity;

namespace SLPreviewBehaviorsLibrary
{
    public class ShakeBehavior : Behavior<UIElement>
    {

        #region Declaration
        private UIElement attachedElement;
        private static Random ranNum = new Random();
        private int CurrentAngle = 0;
        private int CurrentDistant = 0;
        int i = 0;
        #endregion


        #region Property to Expose
        [Category("Animation Properties")]
        public int MaxRotationAngle
        {
            get { return (int)GetValue(MaxRotationAngleProperty); }
            set { SetValue(MaxRotationAngleProperty, value); }
        }

        public static readonly DependencyProperty MaxRotationAngleProperty = DependencyProperty.Register("Maximum Rotation Angle", typeof(int), typeof(ShakeBehavior), new PropertyMetadata(1));

        [Category("Animation Properties")]
        public int MaxMovmentDistant
        {
            get { return (int)GetValue(MaxMovmentDistantProperty); }
            set { SetValue(MaxMovmentDistantProperty, value); }
        }

        public static readonly DependencyProperty MaxMovmentDistantProperty = DependencyProperty.Register("Maximum Movment Distant", typeof(int), typeof(ShakeBehavior), new PropertyMetadata(3));

        #endregion

        protected override void OnAttached()
        {
            attachedElement = this.AssociatedObject;

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            }
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            i++;
            if (i % 10 == 0)
            {
                TransformGroup rt = new TransformGroup();
                RotateTransform newAngle = new RotateTransform();
                TranslateTransform newPosition = new TranslateTransform();

                GeneralTransform objGeneralTransform = attachedElement.TransformToVisual(Application.Current.MainWindow as UIElement);
                Point point = objGeneralTransform.Transform(new Point(0, 0));

                
                Transform oldTransform = attachedElement.RenderTransform;
                if (oldTransform != null)
                {
                    rt.Children.Add(oldTransform);
                }

                int DeltaAngle = ranNum.Next(-MaxRotationAngle - CurrentAngle, (MaxRotationAngle +1) - CurrentAngle);
                newAngle.Angle = DeltaAngle;
                CurrentAngle = CurrentAngle + DeltaAngle;
                int DeltaDistant = ranNum.Next(-MaxMovmentDistant - CurrentDistant, (MaxMovmentDistant +1) - CurrentDistant);
                newPosition.X = DeltaDistant;
                CurrentDistant = CurrentDistant + DeltaDistant;
                newAngle.CenterX = point.X + attachedElement.RenderSize.Width / 2;
                newAngle.CenterY = point.Y + attachedElement.RenderSize.Height / 2;

                rt.Children.Add(newAngle);
                rt.Children.Add(newPosition);
                MatrixTransform mt = new MatrixTransform();
                mt.Matrix = rt.Value;

                attachedElement.RenderTransform = mt;
            }
        }

    }
}
