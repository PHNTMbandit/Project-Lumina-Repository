using UnityEngine;
using DG.Tweening;

namespace BehaviorDesigner.Runtime.Tasks.DOTween
{
    [TaskCategory("DOTween")]
    [TaskDescription("Tween any particular string value.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/DOTween/Editor/Icon.png")]
    public class StringTo : Action
    {
        [Tooltip("The original tween value")]
        public SharedString from;
        [Tooltip("The target tween value")]
        public SharedString to;
        [Tooltip("The time the tween takes to complete")]
        public SharedFloat time;
        [SharedRequired]
        [Tooltip("The stored tweener")]
        public SharedTweener storeTweener;

        private bool complete;

        public override void OnStart()
        {
            storeTweener.Value = DG.Tweening.DOTween.To(() => from.Value, x => from.Value = x, to.Value, time.Value);
            storeTweener.Value.OnComplete(() => complete = true);
        }

        public override TaskStatus OnUpdate()
        {
            return complete ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            complete = false;
        }

        public override void OnReset()
        {
            from = "";
            to = "";
            time = 0;
            storeTweener = null;
        }
    }
}