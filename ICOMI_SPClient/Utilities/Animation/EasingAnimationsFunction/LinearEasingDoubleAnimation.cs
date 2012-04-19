using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using ICOMI_SPClient.Utilities.Animation;

namespace ICOMI_SPClient.Utilities.EasingAnimationsFunction
{

    public class LinearEasingDoubleAnimation : System.Windows.Media.Animation.DoubleAnimationBase
    {
        public static readonly DependencyProperty FromValueProperty =
            DependencyProperty.Register(
                "FromValue", typeof(double), typeof(LinearEasingDoubleAnimation), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ToValueProperty =
            DependencyProperty.Register(
                "ToValue", typeof(double), typeof(LinearEasingDoubleAnimation), new PropertyMetadata(0.0));


        public static readonly DependencyProperty EaseFunctionProperty =
            DependencyProperty.Register(
                "EaseFunction", typeof(EasingMode), typeof(LinearEasingDoubleAnimation), new PropertyMetadata(EasingMode.EaseIn));

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

        public LinearEasingDoubleAnimation()
        {
        }

        public LinearEasingDoubleAnimation(double from, double to, EasingMode easeInMethod, Duration duration)
        {
            FromValue = from;
            ToValue = to;
            Duration = duration;
            EaseFunction = easeInMethod;
        }



        protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, System.Windows.Media.Animation.AnimationClock animationClock)
        {            
            double time = animationClock.CurrentTime.Value.TotalSeconds;
            double startValue = FromValue;
            double eqFinalValue = ToValue - FromValue;
            double duration = Duration.TimeSpan.TotalSeconds;


            return eqFinalValue * time / duration + startValue; 
        }

        protected override Freezable CreateInstanceCore()
        {
            return new QuadEasingDoubleAnimation();
        }
    }
}
