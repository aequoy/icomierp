using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ICOMI_SPClient.Utilities.Animation;

namespace ICOMI_SPClient.Utilities.EasingAnimationsFunction
{

    public class BackEasingDoubleAnimation : System.Windows.Media.Animation.DoubleAnimationBase
    {
        public static readonly DependencyProperty FromValueProperty =
            DependencyProperty.Register(
                "FromValue", typeof(double), typeof(BackEasingDoubleAnimation), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ToValueProperty =
            DependencyProperty.Register(
                "ToValue", typeof(double), typeof(BackEasingDoubleAnimation), new PropertyMetadata(0.0));


        public static readonly DependencyProperty EaseFunctionProperty =
            DependencyProperty.Register(
                "EaseFunction", typeof(EasingMode), typeof(BackEasingDoubleAnimation), new PropertyMetadata(EasingMode.EaseIn));

        public double FromValue
        {
            get
            {
                return (double)GetValue(FromValueProperty);
            }
            set
            {
                SetValue(FromValueProperty, value);
            }
        }

        public double ToValue
        {
            get
            {
                return (double)GetValue(ToValueProperty);
            }
            set
            {
                SetValue(ToValueProperty, value);
            }
        }

        public EasingMode EaseFunction
        {
            get
            {
                return (EasingMode)GetValue(EaseFunctionProperty);
            }
            set
            {
                SetValue(EaseFunctionProperty, value);
            }
        }

        public BackEasingDoubleAnimation()
        {
        }

        public BackEasingDoubleAnimation(double from, double to, EasingMode easeInMethod, Duration duration)
        {
            FromValue = from;
            ToValue = to;
            Duration = duration;
            EaseFunction = easeInMethod;
        }



        protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, System.Windows.Media.Animation.AnimationClock animationClock)
        {
            double returnValue = 0;

            double time = animationClock.CurrentTime.Value.TotalSeconds;
            double startValue = FromValue;
            double eqFinalValue = ToValue - FromValue;
            double duration = Duration.TimeSpan.TotalSeconds;


            switch (this.EaseFunction)
            {
                case EasingMode.EaseIn:
                    returnValue = eqFinalValue * (time /= duration) * time * ((1.70158 + 1) * time - 1.70158) + startValue;
                    break;
                case EasingMode.EaseOut:
                    returnValue = eqFinalValue * ((time = time / duration - 1) * time * ((1.70158 + 1) * time + 1.70158) + 1) + startValue;
                    break;
                case EasingMode.EaseInOut:
                    double s = 1.70158;
                    if ((time /= duration / 2) < 1)
                    {
                        returnValue = eqFinalValue / 2 * (time * time * (((s *= (1.525)) + 1) * time - s)) + startValue;
                    }
                    else
                    {
                        returnValue = eqFinalValue / 2 * ((time -= 2) * time * (((s *= (1.525)) + 1) * time + s) + 2) + startValue;
                    }
                    break;
                default:
                    break;
            }

            return returnValue;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new QuadEasingDoubleAnimation();
        }
    }
}
