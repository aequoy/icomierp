using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;
using ICOMI_SPClient.Utilities.Animation;

namespace ICOMI_SPClient.Utilities.EasingAnimationsFunction
{

    public class QuadPointEasingAnimation : PointAnimationBase
    {
        public static readonly DependencyProperty FromValueProperty =
            DependencyProperty.Register(
                "FromValue", typeof(Point), typeof(QuadPointEasingAnimation), new PropertyMetadata(new Point(0,0)));

        public static readonly DependencyProperty ToValueProperty =
            DependencyProperty.Register(
                "ToValue", typeof(Point), typeof(QuadPointEasingAnimation), new PropertyMetadata(new Point(0, 0)));


        public static readonly DependencyProperty EaseFunctionProperty =
            DependencyProperty.Register(
                "EaseFunction", typeof(PointEasingMode), typeof(QuadPointEasingAnimation), new PropertyMetadata(PointEasingMode.EaseInOut));

        public Point FromValue
        {
            get
            {
                return (Point)GetValue(FromValueProperty);
            }
            set
            {
                SetValue(FromValueProperty, value);
            }
        }

        public Point ToValue
        {
            get
            {
                return (Point)GetValue(ToValueProperty);
            }
            set
            {
                SetValue(ToValueProperty, value);
            }
        }

        public PointEasingMode EaseFunction
        {
            get
            {
                return (PointEasingMode)GetValue(EaseFunctionProperty);
            }
            set
            {
                SetValue(EaseFunctionProperty, value);
            }
        }

        public QuadPointEasingAnimation()
        {
        }

        /// <summary>
        /// Set Properties
        /// </summary>
        /// <param name="from">Start Point</param>
        /// <param name="to">End Point</param>
        /// <param name="easeInMethod">Easing method equation</param>
        /// <param name="duration">Duration for the animation</param>
        public QuadPointEasingAnimation(Point from, Point to, PointEasingMode easeInMethod, Duration duration)
        {
            FromValue = from;
            ToValue = to;
            Duration = duration;
            EaseFunction = easeInMethod;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new QuadPointEasingAnimation();
        }

        /// <summary>
        /// Work out which type of animation is needed
        /// </summary>
        /// <param name="defaultOriginValue">default start value</param>
        /// <param name="defaultDestinationValue">default end value</param>
        /// <param name="animationClock">Animaiton to run</param>
        /// <returns></returns>
        protected override Point GetCurrentValueCore(Point defaultOriginValue, Point defaultDestinationValue, AnimationClock animationClock)
        {            
            double returnValueX = 0;
            double returnValueY = 0;

            double time = animationClock.CurrentTime.Value.TotalSeconds;
            double startValueX = FromValue.X;
            double eqFinalValueX = ToValue.X - FromValue.X;

            double startValueY = FromValue.Y;
            double eqFinalValueY = ToValue.Y - FromValue.Y;
            double duration = Duration.TimeSpan.TotalSeconds;


            switch (this.EaseFunction)
            {
                case PointEasingMode.EaseIn:
                    returnValueX = eqFinalValueX * (time /= duration) * time + startValueX;
                    returnValueY = eqFinalValueY * (time /= duration) * time + startValueY;
                    break;
                case PointEasingMode.EaseOut:
                    returnValueX = -eqFinalValueX * (time /= duration) * (time - 2) + startValueX;
                    returnValueY = -eqFinalValueY * (time /= duration) * (time - 2) + startValueY;
                    break;
                case PointEasingMode.EaseInOut:
                    if ((time /= duration / 2) < 1)
                    {
                        returnValueX = eqFinalValueX / 2 * time * time + startValueX;
                        returnValueY = eqFinalValueY / 2 * time * time + startValueY;
                    }
                    else
                    {
                        returnValueX = -eqFinalValueX / 2 * ((--time) * (time - 2) - 1) + startValueX;
                        returnValueY = -eqFinalValueY / 2 * ((--time) * (time - 2) - 1) + startValueY;
                    }
                    break;
                default:
                    break;
            }

            return new Point(returnValueX, returnValueY);
        }        
    }
}
