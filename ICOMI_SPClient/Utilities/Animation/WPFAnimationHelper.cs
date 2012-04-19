using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;
using ICOMI_SPClient.Utilities.EasingAnimationsFunction;

namespace ICOMI_SPClient.Utilities.Animation
{
    public enum EasingFunction
    {
        Quad,
        Linear,
        Back,
        Expo,
        Elastic,
        Sine
    }

    public enum EasingMode
    {
        EaseIn,
        EaseOut,
        EaseInOut
    }

    /// <summary>
    /// Different animation modes
    /// </summary>
    public enum PointEasingMode
    {
        EaseIn,
        EaseOut,
        EaseInOut
    }

    public class WPFAnimationHelper
    {


        public WPFAnimationHelper()
        {
        }


        public static AnimationClock AnimateEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            EasingFunction function,            
            double to,
            int durationMS,
            EventHandler callbackFunc)
        {
            double from = double.IsNaN((double)element.GetValue(prop)) ?
                                0 :
                                (double)element.GetValue(prop);

            AnimationTimeline timeline = GetEasingAnimation(function, EasingMode.EaseIn, from, to, durationMS);
            return Animate(element, prop, timeline, durationMS, null, null, callbackFunc);
        }        

        public static AnimationClock AnimateEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            EasingFunction function,            
            double to,
            int durationMS)
        {
            double from = double.IsNaN((double)element.GetValue(prop)) ?
                                0 :
                                (double)element.GetValue(prop);

            AnimationTimeline timeline = GetEasingAnimation(function, EasingMode.EaseIn, from, to, durationMS);
            return Animate(element, prop, timeline, durationMS, null, null, null);
        }

        public static AnimationClock AnimateEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            EasingFunction function,
            EasingMode mode,            
            double to,
            int durationMS)
        {
            double from = double.IsNaN((double)element.GetValue(prop)) ?
                                0 :
                                (double)element.GetValue(prop);

            AnimationTimeline timeline = GetEasingAnimation(function, mode, from, to, durationMS);
            return Animate(element, prop, timeline, durationMS, null, null, null);
        }

        public static AnimationClock AnimateEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            EasingFunction function,
            EasingMode mode,            
            double to,
            int durationMS,
            EventHandler callbackFunc)
        {
            double from = double.IsNaN((double)element.GetValue(prop)) ?
                                 0 :
                                 (double)element.GetValue(prop);

            AnimationTimeline timeline = GetEasingAnimation(function, mode, from, to, durationMS);
            return Animate(element, prop, timeline, durationMS, null, null, callbackFunc);
        }        

        public static AnimationClock AnimateEasingEquation(
            DependencyObject element,
            DependencyProperty prop,
            EasingFunction function,
            EasingMode mode,
            double? from,
            double to,
            int durationMS,
            EventHandler callbackFunc)
        {
            double defaultFrom = double.IsNaN((double)element.GetValue(prop)) ?
                                0 :
                                (double)element.GetValue(prop);

            AnimationTimeline timeline = GetEasingAnimation(function, mode, from.GetValueOrDefault(defaultFrom), to, durationMS);
            return Animate(element, prop, timeline, durationMS, null, null, callbackFunc);
        }

        private static AnimationTimeline GetEasingAnimation(EasingFunction function, EasingMode mode, double from, double to, int durationMS)
        {
            AnimationTimeline returnTimeline = null;
            switch (function)
            {
                case EasingFunction.Quad:
                    returnTimeline = new QuadEasingDoubleAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                case EasingFunction.Linear:
                    returnTimeline = new LinearEasingDoubleAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                case EasingFunction.Back:
                    returnTimeline = new BackEasingDoubleAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                case EasingFunction.Expo:
                    returnTimeline = new ExpoEasingDoubleAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                case EasingFunction.Elastic:
                    returnTimeline = new ElasticEasingDoubleAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                case EasingFunction.Sine:
                    returnTimeline = new SineEasingDoubleAnimation(from, to, mode, new Duration(new TimeSpan(0, 0, 0, 0, durationMS)));
                    break;
                default:
                    break;
            }

            return returnTimeline;
        }

        private static AnimationClock Animate(
            DependencyObject animatable,
            DependencyProperty prop,
            AnimationTimeline anim,
            int duration,
            double? acceleration,
            double? deceleration,
            EventHandler func
            )
        {
            if (acceleration.HasValue)
            {
                anim.AccelerationRatio = acceleration.GetValueOrDefault(0);
            }

            if (deceleration.HasValue)
            {
                anim.DecelerationRatio = deceleration.GetValueOrDefault(0);
            }

            anim.Duration = TimeSpan.FromMilliseconds(duration);
            anim.Freeze();

            AnimationClock animClock = anim.CreateClock();

            // When animation is complete, remove animation and set the animation's "To" 
            // value as the new value of the property.
            EventHandler eh = null;
            eh = delegate(object sender, EventArgs e)
            {
                animatable.SetValue(prop, animatable.GetValue(prop));

                ((IAnimatable)animatable).ApplyAnimationClock(prop, null);

                animClock.Completed -= eh;
            };

            animClock.Completed += eh;

            // assign completed eventHandler, if defined
            if (func != null)
                animClock.Completed += func;

            animClock.Controller.Begin();

            // goferit
            ((IAnimatable)animatable).ApplyAnimationClock(prop, animClock);

            return animClock;

        }

    }
}

