using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ICOMI_SPClient.Utilities.Animation;

namespace ICOMI_SPClient.Utilities.EasingAnimationsFunction
{

    public class ElasticEasingDoubleAnimation : System.Windows.Media.Animation.DoubleAnimationBase
    {
        public static readonly DependencyProperty FromValueProperty =
            DependencyProperty.Register(
                "FromValue", typeof(double), typeof(ElasticEasingDoubleAnimation), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ToValueProperty =
            DependencyProperty.Register(
                "ToValue", typeof(double), typeof(ElasticEasingDoubleAnimation), new PropertyMetadata(0.0));


        public static readonly DependencyProperty EaseFunctionProperty =
            DependencyProperty.Register(
                "EaseFunction", typeof(EasingMode), typeof(ElasticEasingDoubleAnimation), new PropertyMetadata(EasingMode.EaseIn));

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

        public ElasticEasingDoubleAnimation()
        {
        }

        public ElasticEasingDoubleAnimation(double from, double to, EasingMode easeInMethod, Duration duration)
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
                    if ((time /= duration) == 1)
                    {
                        returnValue = startValue + eqFinalValue;
                    }
                    else
                    {
                        double p = duration * .3;
                        double s = p / 4;

                        returnValue = -(eqFinalValue * Math.Pow(2, 10 * (time -= 1)) * Math.Sin((time * duration - s) * (2 * Math.PI) / p)) + startValue;
                    }
                    break;
                case EasingMode.EaseOut:
                    if ((time /= duration) == 1)
                    {
                        returnValue = startValue + eqFinalValue;
                    }
                    else
                    {
                        double p = duration * .3;
                        double s = p / 4;

                        returnValue = (eqFinalValue * Math.Pow(2, -10 * time) * Math.Sin((time * duration - s) * (2 * Math.PI) / p) + eqFinalValue + startValue);
                    }
                    break;
                case EasingMode.EaseInOut:
                    if ((time /= duration / 2) == 2)
                    {
                        returnValue = startValue + eqFinalValue;
                    }
                    else
                    {
                        double p = duration * (.3 * 1.5);
                        double s = p / 4;

                        if (time < 1)
                        {
                            returnValue = -.5 * (eqFinalValue * Math.Pow(2, 10 * (time -= 1)) * Math.Sin((time * duration - s)
                                * (2 * Math.PI) / p)) + startValue;
                        }
                        else
                        {
                            returnValue = eqFinalValue * Math.Pow(2, -10 * (time -= 1)) * Math.Sin((time * duration - s)
                                * (2 * Math.PI) / p) * .5 + eqFinalValue + startValue;
                        }
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
