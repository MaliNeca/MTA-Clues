using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{


    [Tooltip("Set starting page index - starting from 0")]
    public int startingPage = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;
    [Tooltip("Button to go to the previous page (optional)")]
    public GameObject prevButton;
    [Tooltip("Button to go to the next page (optional)")]
    public GameObject nextButton;
    [Tooltip("Sprite for unselected page (optional)")]
    public Image unselectedPage;
    [Tooltip("Sprite for selected page (optional)")]
    public Image selectedPage;
    [Tooltip("Container with page images (optional)")]
    public Transform pageSelectionIcons;
    [Tooltip("Clue text field")]
    public TextMeshProUGUI clue;
    [Tooltip("Image for multiplayer")]
    public GameObject playerOneContainer;
    public GameObject playerTwoContainer;
    public GameObject playerThreeContainer;
    public GameObject playerFourContainer;
    public GameObject playerQuestionsContainer;
    [Tooltip("Animators")]
    public Animator TopLinesAnimator;



    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int _fastSwipeThresholdMaxLimit;

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;
    private RectTransform _clues;

    private bool _horizontal;

    // number of pages in container
    private int _pageCount;
    private int _currentPage;

    // whether lerping is in progress and target lerp position
    private bool _lerp;
    private Vector2 _lerpTo;

    // target position of every page
    private List<Vector2> _pagePositions = new List<Vector2>();

    // in draggging, when dragging started and where it started
    private bool _dragging;
    private float _timeStamp;
    private Vector2 _startPosition;

    // for showing small page icons
    private bool _showPageSelection;
    private int _previousPageSelectionIndex;
    // container with Image components - one Image for each page
    private List<Image> _pageSelectionImages;

    //container with Clues
    /*private string[] playerOneClues = { "Trial-CV-CM-112020 is currently 26% effective.",
                                        "Columbia Medical say progress on Trial-CV-CM-112020 is 3x faster than usual, and by the time it is developed it will be 3 times more effective than it is now.",
                                        "Columbia Medical predict it will take 18 months to complete their vaccine development.",
                                        "Trial-CV-CM-112020 is stored in the refrigeration unit at Columbia Medical.",
                                        "Columbia Medical have produced a standard batch of 15'000 vaccines.",
                                        "The projected cost per batch of Trial-CV-CM-112020 is $22'500"
                                        };
    private string[] playerTwoClues = { "Colombia Medicine have recently been able to double their vaccine",
                                        "Previously, Generator was only 47% effective",
                                        "Colombia Medicine anticipate their vaccine will be ready in 8 months’ time",
                                        "Generator is stored in the secure refrigeration unit at the facility",
                                        "The currently cost per dose of vaccine 2 is $2.25",
                                        "By the time of manufacturing Colombia Medicine predict their new machinery will reduce the cost per dose by 75%"
                                        };
    private string[] playerThreeClues = {"Currently Elizabeth is 51% effective, and we have 3 further iterations of the vaccine planned",
                                        "Each iteration of Elizabeth has so far has increase its effectiveness by 6% points",
                                        "The final version of Elizabeth will be available in 5 months’ time",
                                        "Elizabeth is stable when refrigerated",
                                        "The test run by Eton Innovation show that Elizabeth is stable above 5 degrees C",
                                        "Eton Innovation project the cost per dose of their vaccine to be less than one 1$"
                                        };
    private string[] playerFourClues = {"The Viribus is 91% effective",
                                        "Boston Vaccines are very confident their vaccine will be ready in 9 months’ time",
                                        "Charlie Hainsworth says they can half the speed of development for a cost per dose increase of $0.15",
                                        "Charlie Hainsworth of Boston Vaccines shows the vaccine is effective when refrigerated",
                                        "Recent tests show that Viribus is stable up to 50 degrees C",
                                        "The projected cost per dose of Viribus is $.80"
                                        };*/
    private string[] playerOneClues;
    private string[] playerTwoClues;
    private string[] playerThreeClues;
    private string[] playerFourClues;
    private string[] playerQuestions;

  

    void Start()
    {
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        switch (LoadScene.playerID)
        {
            case 1:
                {
                    _scrollRectComponent.content = playerOneContainer.GetComponent<RectTransform>();
                    playerOneClues = LoadScene.playerOneClues;
                    playerTwoContainer.SetActive(false);
                    playerThreeContainer.SetActive(false);
                    playerFourContainer.SetActive(false);
                    playerQuestionsContainer.SetActive(false);
                    break;
                }
            case 2:
                {
                    
                    _scrollRectComponent.content = playerTwoContainer.GetComponent<RectTransform>();
                    playerTwoClues = LoadScene.playerTwoClues;
                    playerOneContainer.SetActive(false);
                    playerThreeContainer.SetActive(false);
                    playerFourContainer.SetActive(false);
                    playerQuestionsContainer.SetActive(false);
                    break;
                }
            case 3:
                {
                    //_scrollRectComponent.content = playerThreeContainer;
                    _scrollRectComponent.content = playerThreeContainer.GetComponent<RectTransform>();
                    playerThreeClues = LoadScene.playerThreeClues;
                    playerOneContainer.SetActive(false);
                    playerTwoContainer.SetActive(false);
                    playerFourContainer.SetActive(false);
                    playerQuestionsContainer.SetActive(false);
                    break;
                }
            case 4:
                {
                    // _scrollRectComponent.content = playerFourContainer;
                    _scrollRectComponent.content = playerFourContainer.GetComponent<RectTransform>();
                    playerFourClues = LoadScene.playerFourClues;
                    playerOneContainer.SetActive(false);
                    playerTwoContainer.SetActive(false);
                    playerThreeContainer.SetActive(false);
                    playerQuestionsContainer.SetActive(false);
                    break;
                }
            case 5:
                _scrollRectComponent.content = playerQuestionsContainer.GetComponent<RectTransform>();
                playerQuestions = LoadScene.playerQuestions;
                playerOneContainer.SetActive(false);
                playerTwoContainer.SetActive(false);
                playerThreeContainer.SetActive(false);
                playerFourContainer.SetActive(false);
                break;
            default:
                {
                    Debug.LogError("Player index was not found");
                    break;
                }
        }
        _container = _scrollRectComponent.content;
        _pageCount = _container.childCount;
      
        // is it horizontal or vertical scrollrect
        if (_scrollRectComponent.horizontal && !_scrollRectComponent.vertical)
        {
            _horizontal = true;
        }
        else if (!_scrollRectComponent.horizontal && _scrollRectComponent.vertical)
        {
            _horizontal = false;
        }
        else
        {
            Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
            _horizontal = true;
        }

        _lerp = false;

        // init
        SetPagePositions();
        SetPage(startingPage);
        SetClue(startingPage);
        InitPageSelection();
        SetPageSelection(startingPage);

        // prev and next buttons
        if (nextButton)
            nextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });

        if (prevButton)
            prevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });
    }

    //------------------------------------------------------------------------
    void Update()
    {
        // if moving to target position
        if (_lerp)
        {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
            {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }

            // switches selection icon exactly to correct page
            if (_showPageSelection)
            {
                SetPageSelection(GetNearestPage());
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetPagePositions()
    {
        int width = 0;
        int height = 0;
        int offsetX = 0;
        int offsetY = 0;
        int containerWidth = 0;
        int containerHeight = 0;

        if (_horizontal)
        {
            // screen width in pixels of scrollrect window
            width = (int)_scrollRectRect.rect.width;
            // center position of all pages
            offsetX = width / 2;
            // total width
            containerWidth = width * _pageCount;
            // limit fast swipe length - beyond this length it is fast swipe no more
            _fastSwipeThresholdMaxLimit = width;
        }
        else
        {
            height = (int)_scrollRectRect.rect.height;
            offsetY = height / 2;
            containerHeight = height * _pageCount;
            _fastSwipeThresholdMaxLimit = height;
        }

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        // delete any previous settings
        _pagePositions.Clear();

        // iterate through all container childern and set their positions
        for (int i = 0; i < _pageCount; i++)
        {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            if (_horizontal)
            {
                childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
            }
            else
            {
                childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));
            }
            child.anchoredPosition = childPosition;
            _pagePositions.Add(-childPosition);
        }
    }

    //------------------------------------------------------------------------
    private void SetPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _container.anchoredPosition = _pagePositions[aPageIndex];
        _currentPage = aPageIndex;
    }

    public void SetClue(int aPageIndex)
    {
        switch (LoadScene.playerID)
        {
            case 1:
                {
                    SetDotsCount.Instance.Set8();
                    //set clue for player One
                    switch (aPageIndex)
                    {

                        case 0:
                            {   //set first clue on page 1
                                clue.text = playerOneClues[0];
                                break;
                            }
                        case 1:
                            {
                                //set second clue on page 2 
                                clue.text = playerOneClues[1];
                                break;
                            }
                        case 2:
                            {
                                //set third clue on page 3
                                clue.text = playerOneClues[2];
                                break;
                            }
                        case 3:
                            {
                                //set fourth clue on page 4
                                clue.text = playerOneClues[3];
                                break;
                            }
                        case 4:
                            {
                                //set fifth clue on page 5
                                clue.text = playerOneClues[4];
                                break;
                            }
                        case 5:
                            {
                                //set sixth clue on page 6
                                clue.text = playerOneClues[5];
                                break;
                            }
                        case 6:
                            {
                                //set sixth clue on page 6
                                clue.text = playerOneClues[6];
                                break;
                            }
                        case 7:
                            {
                                //set sixth clue on page 6
                                clue.text = playerOneClues[7];
                                break;
                            }

                        default: break;
                    }


                    break;
                }
            case 2:
                {
                    SetDotsCount.Instance.Set8();
                    //set clue for player Two
                    switch (aPageIndex)
                    {

                        case 0:
                            {   //set first clue on page 1
                                clue.text = playerTwoClues[0];
                                break;
                            }
                        case 1:
                            {
                                //set second clue on page 2 
                                clue.text = playerTwoClues[1];
                                break;
                            }
                        case 2:
                            {
                                //set third clue on page 3
                                clue.text = playerTwoClues[2];
                                break;
                            }
                        case 3:
                            {
                                //set fourth clue on page 4
                                clue.text = playerTwoClues[3];
                                break;
                            }
                        case 4:
                            {
                                //set fifth clue on page 5
                                clue.text = playerTwoClues[4];
                                break;
                            }
                        case 5:
                            {
                                //set sixth clue on page 6
                                clue.text = playerTwoClues[5];
                                break;
                            }
                        case 6:
                            {
                                //set sixth clue on page 6
                                clue.text = playerTwoClues[6];
                                break;
                            }
                        case 7:
                            {
                                //set sixth clue on page 6
                                clue.text = playerTwoClues[7];
                                break;
                            }

                        default: break;
                    }
                    break;
                }
            case 3:
                {
                    //set clue for player Three

                    switch (aPageIndex)
                    {

                        case 0:
                            {   //set first clue on page 1
                                clue.text = playerThreeClues[0];
                                break;
                            }
                        case 1:
                            {
                                //set second clue on page 2 
                                clue.text = playerThreeClues[1];
                                break;
                            }
                        case 2:
                            {
                                //set third clue on page 3
                                clue.text = playerThreeClues[2];
                                break;
                            }
                        case 3:
                            {
                                //set fourth clue on page 4
                                clue.text = playerThreeClues[3];
                                break;
                            }
                        case 4:
                            {
                                //set fifth clue on page 5
                                clue.text = playerThreeClues[4];
                                break;
                            }
                        case 5:
                            {
                                //set sixth clue on page 6
                                clue.text = playerThreeClues[5];
                                break;
                            }
                        case 6:
                            {
                                //set sixth clue on page 6
                                clue.text = playerThreeClues[6];
                                break;
                            }
                        case 7:
                            {
                                //set sixth clue on page 6
                                clue.text = playerThreeClues[7];
                                break;
                            }

                        default: break;
                    }
                    break;

                }
            case 4:
                {
                    //set clue for player Four
                    switch (aPageIndex)
                    {

                        case 0:
                            {   //set first clue on page 1
                                clue.text = playerFourClues[0];
                                break;
                            }
                        case 1:
                            {
                                //set second clue on page 2 
                                clue.text = playerFourClues[1];
                                break;
                            }
                        case 2:
                            {
                                //set third clue on page 3
                                clue.text = playerFourClues[2];
                                break;
                            }
                        case 3:
                            {
                                //set fourth clue on page 4
                                clue.text = playerFourClues[3];
                                break;
                            }
                        case 4:
                            {
                                //set fifth clue on page 5
                                clue.text = playerFourClues[4];
                                break;
                            }
                        case 5:
                            {
                                //set sixth clue on page 6
                                clue.text = playerFourClues[5];
                                break;
                            }
                        case 6:
                            {
                                //set sixth clue on page 6
                                clue.text = playerFourClues[6];
                                break;
                            }
                        case 7:
                            {
                                //set sixth clue on page 6
                                clue.text = playerFourClues[7];
                                break;
                            }

                        default: break;
                    }

                    break;
                }
            case 5:
                {
                    //set clue for player Four
                    switch (aPageIndex)
                    {

                        case 0:
                            {   //set first clue on page 1
                                clue.text = playerQuestions[0];
                                break;
                            }
                        case 1:
                            {
                                //set second clue on page 2 
                                clue.text = playerQuestions[1];
                                break;
                            }
                        case 2:
                            {
                                //set third clue on page 3
                                clue.text = playerQuestions[2];
                                break;
                            }
                        case 3:
                            {
                                //set fourth clue on page 4
                                clue.text = playerQuestions[3];
                                break;
                            }
                        case 4:
                            {
                                //set fifth clue on page 5
                                clue.text = playerQuestions[4];
                                break;
                            }
                        case 5:
                            {
                                //set sixth clue on page 6
                                clue.text = playerQuestions[5];
                                break;
                            }


                        default: break;
                    }

                    break;
                }
            default:
                {
                    Debug.LogError("Player index was not found");
                    break;
                }


        }
        
    }

    //------------------------------------------------------------------------
    private void LerpToPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _lerpTo = _pagePositions[aPageIndex];
        _lerp = true;
        _currentPage = aPageIndex;
        SetClue(_currentPage);
    }

    //------------------------------------------------------------------------
    private void InitPageSelection()
    {
        // page selection - only if defined sprites for selection icons
        _showPageSelection = unselectedPage != null && selectedPage != null;
        if (_showPageSelection)
        {
            // also container with selection images must be defined and must have exatly the same amount of items as pages container
            if (pageSelectionIcons == null || pageSelectionIcons.childCount != _pageCount)
            {
                Debug.LogWarning("Different count of pages and selection icons - will not show page selection");
                _showPageSelection = false;
            }
            else
            {
                _previousPageSelectionIndex = -1;
                _pageSelectionImages = new List<Image>();

                // cache all Image components into list
                for (int i = 0; i < pageSelectionIcons.childCount; i++)
                {
                    Image image = pageSelectionIcons.GetChild(i).GetComponent<Image>();
                    if (image == null)
                    {
                        Debug.LogWarning("Page selection icon at position " + i + " is missing Image component");
                    }
                    _pageSelectionImages.Add(image);
                }
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetPageSelection(int aPageIndex)
    {
        // nothing to change
        if (_previousPageSelectionIndex == aPageIndex)
        {
            return;
        }

        // unselect old
        if (_previousPageSelectionIndex >= 0)
        {
            _pageSelectionImages[_previousPageSelectionIndex].transform.GetChild(0).gameObject.SetActive(false);
            //_pageSelectionImages[_previousPageSelectionIndex].SetNativeSize();
        }
        // select new
        _pageSelectionImages[aPageIndex].transform.GetChild(0).gameObject.SetActive(true);
        //_pageSelectionImages[aPageIndex].SetNativeSize();

        _previousPageSelectionIndex = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void NextScreen()
    {
        AudioController.Instance.PlayClick();
        TopLinesAnimator.SetTrigger("open");
        LerpToPage(_currentPage + 1);
    }

    //------------------------------------------------------------------------
    private void PreviousScreen()
    {
        Debug.Log("CURRENT PAGE: " + _currentPage);
        if (_currentPage >0)
        {
            AudioController.Instance.PlayClick();
            TopLinesAnimator.SetTrigger("open");
        }
        LerpToPage(_currentPage - 1);
       

    }

    //------------------------------------------------------------------------
    private int GetNearestPage()
    {
        // based on distance from current position, find nearest page
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = _currentPage;

        for (int i = 0; i < _pagePositions.Count; i++)
        {
            float testDist = Vector2.SqrMagnitude(currentPosition - _pagePositions[i]);
            if (testDist < distance)
            {
                distance = testDist;
                nearestPage = i;
            }
        }

        return nearestPage;
    }

    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData)
    {
        // if currently lerping, then stop it as user is draging
        _lerp = false;
        // not dragging yet
        _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData)
    {
        // how much was container's content dragged
        float difference;
        if (_horizontal)
        {
            difference = _startPosition.x - _container.anchoredPosition.x;
        }
        else
        {
            difference = -(_startPosition.y - _container.anchoredPosition.y);
        }

        // test for fast swipe - swipe that moves only +/-1 item
        if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime &&
            Mathf.Abs(difference) > fastSwipeThresholdDistance &&
            Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit)
        {
            if (difference > 0)
            {
                NextScreen();
            }
            else
            {
                PreviousScreen();
            }
        }
        else
        {
            // if not fast time, look to which page we got to
            LerpToPage(GetNearestPage());
        }

        _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData)
    {
        if (!_dragging)
        {
            // dragging started
            _dragging = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            _timeStamp = Time.unscaledTime;
            // save current position of cointainer
            _startPosition = _container.anchoredPosition;
        }
        else
        {
            if (_showPageSelection)
            {
                SetPageSelection(GetNearestPage());
            }
        }
    }
}


