using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class AnimationManager : MonoBehaviour
{
    //public string selectedAnimation;

    // private Animator lastSelection;
    [ReadOnly] public int firstTime;

    public void TriggerAnimation(GameObject gameObject)
    {
        EmojiInfo emjInfo = gameObject.GetComponent<EmojiInfo>();
        emjInfo.ID = gameObject.GetComponent<RectTransform>().GetSiblingIndex();
        firstTime += 1;
        if ((firstTime == 1 || firstTime == 2) /*&& emjInfo.ID == 0*/)
        {
            if (emjInfo.EmojiSide == ItemSide.right)
            {
                if (MergeManager.Instance)
                {
                    MergeManager.Instance.rightEmoji = emjInfo;

                }
            }
            if (emjInfo.EmojiSide == ItemSide.left)
            {
                if (MergeManager.Instance)
                {
                    MergeManager.Instance.leftEmoji = emjInfo;

                }
            }
            return;
        }

        SoundsManager.instSound.PlayTheSound(soundType.emojiPop);
        MergeManager.Instance.EnableButtons();
        if (emjInfo.EmojiSide == ItemSide.left)
        {
            gameObject.GetComponent<DOTweenAnimation>().DORewindAllById("ScaleUpItem");
            gameObject.GetComponent<DOTweenAnimation>().DORestartById("ScaleUpItem");
            MergeManager.Instance.leftEmoji = emjInfo;
            if (TutorialHandler.Instance.indexer == 3) MergeManager.Instance.leftScroll.InstantScrollTo(emjInfo.ID);

        }
        if (emjInfo.EmojiSide == ItemSide.right)
        {
            gameObject.GetComponent<DOTweenAnimation>().DORewindAllById("ScaleUpItemRight");
            gameObject.GetComponent<DOTweenAnimation>().DORestartById("ScaleUpItemRight");
            MergeManager.Instance.rightEmoji = emjInfo;
            if (TutorialHandler.Instance.indexer == 4) MergeManager.Instance.rightScroll.InstantScrollTo(emjInfo.ID);
        }

    }

    //public void TriggerAnimation(GameObject gameObject)
    //{
    //    Animator objAnimator = null;
    //    if (gameObject != null)
    //        objAnimator = gameObject.GetComponent<Animator>();


    //    if (lastSelection != null && objAnimator != lastSelection)
    //    {
    //        lastSelection.SetBool(selectedAnimation, false);
    //    }

    //    if (objAnimator != null)
    //    {
    //        objAnimator.SetBool(selectedAnimation, true);
    //    }

    //    lastSelection = objAnimator;
    //}

    //public void TriggerAnimation (int index)
    //{

    //}
}
