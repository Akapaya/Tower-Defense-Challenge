using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [Header("Main Menu Components")]
    [SerializeField] private RectTransform _title;
    [SerializeField] private RectTransform _buttonsPanel;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

    [Header("Main Menu Animation Positions")]
    [SerializeField] private Vector2 _titleOnScreenPosition;
    [SerializeField] private Vector2 _buttonsPanelOnScreenPosition;
    [SerializeField] private Vector2 _titleOffScreenPosition;
    [SerializeField] private Vector2 _buttonsPanelOffScreenPosition;

    [Header("Select Stage Components")]
    [SerializeField] private RectTransform _selectStagePanel;

    [Header("Select Stage Animation Positions")]
    [SerializeField] private Vector2 _selectStageOnScreenPosition;
    [SerializeField] private Vector2 _selectStageOffScreenPosition;

    public void StartMenuAnimation()
    {
        _verticalLayoutGroup.enabled = false;

        _title.anchoredPosition = _titleOffScreenPosition;
        _title.DOAnchorPos(_titleOnScreenPosition, 2f).SetEase(Ease.OutBounce)
            .OnComplete(() => _verticalLayoutGroup.enabled = true);

        _buttonsPanel.anchoredPosition = _buttonsPanelOffScreenPosition;
        _buttonsPanel.DOAnchorPos(_buttonsPanelOnScreenPosition, 2f).SetEase(Ease.OutBounce);
    }

    public void HideMenuAnimation()
    {
        _verticalLayoutGroup.enabled = false;

        _title.DOAnchorPos(_titleOffScreenPosition, 1f).SetEase(Ease.InBack);

        _buttonsPanel.DOAnchorPos(_buttonsPanelOffScreenPosition, 1f).SetEase(Ease.InBack);
    }

    public void ShowSelectStage()
    {
        _selectStagePanel.DOAnchorPos(_selectStageOnScreenPosition, 1f).SetEase(Ease.InBack);
    }

    public void HideSelectStage()
    {
        _selectStagePanel.DOAnchorPos(_selectStageOffScreenPosition, 1f).SetEase(Ease.InBack);
    }
}
