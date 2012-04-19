using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ICOMI_SPClient.Utilities.Animation;

namespace ICOMI_SPClient.Utilities.EasingAnimationsFunction
{

    public class SineEasingDoubleAnimation : System.Windows.Media.Animation.DoubleAnimationBase
    {
        public static readonly DependencyProperty FromValueProperty =
            DependencyProperty.Register(
                "FromValue", typeof(double), typeof(SineEasingDoubleAnimation), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ToValueProperty =
            DependencyProperty.Register(
                "ToValue", typeof(double), typeof(SineEasingDoubleAnimation), new PropertyMetadata(0.0));


        public static readonly DependencyProperty EaseFunctionProperty =
            DependencyProperty.Register(
                "EaseFunction", typeof(EasingMode), typeof(SineEasingDoubleAnimation), new PropertyMetadata(EasingMode.EaseIn));

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

        public SineEasingDoubleAnimation()
        {
        }

        public SineEasingDoubleAnimation(double from, double to, EasingMode easeInMethod, Duration duration)
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
                    returnValue = -eqFinalValue * Math.Cos(time / duration * (Math.PI / 2)) + eqFinalValue + startValue;
                    break;
                case EasingMode.EaseOut:
                    returnValue =  eqFinalValue * Math.Sin(time / duration * (Math.PI / 2)) + startValue;
                    break;
                case EasingMode.EaseInOut:
                    if ((time /= duration / 2) < 1)
                    {
                        returnValue = eqFinalValue / 2 * (Math.Sin(Math.PI * time / 2)) + startValue;
                    }
                    else
                    {
                        returnValue = -eqFinalValue / 2 * (Math.Cos(Math.PI * --time / 2) - 2) + startValue;
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
