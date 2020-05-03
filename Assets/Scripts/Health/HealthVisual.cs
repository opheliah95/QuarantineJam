using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HealthVisual : MonoBehaviour
{
    public int heartCount = 3;
    public float XPos = 0;
    [SerializeField]
    public float distanceBtwHearts = 50;
    public float sizeDelta = 50;


    // hearts
    [SerializeField]
    private Sprite emptyheartSprite;

    [SerializeField]
    private Sprite halfHeartSprite;

    [SerializeField]
    private Sprite fullHeartSprite;

    [SerializeField]
    private List<HeartImage> heartImageList;


    /*
     * HeartsHealthSystem 
     * it is static, you can all it anywhere from any script...
     * 
     * by calling HealthVisual.heartHealthSyst
     * 
     * f
     * 
     * */

    public static HeartsHealthSystem heartHealthSystem; 


    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        heartHealthSystem = new HeartsHealthSystem(heartCount); // spawn 3 hearts
        // display hearts visually
        setHeartHealthSystem(heartHealthSystem);

    }

    public void setHeartHealthSystem(HeartsHealthSystem sys)
    {
        heartHealthSystem = sys;
        List<HeartsHealthSystem.Heart> allHearts = sys.getHeartsList();
        for(int i = 0; i < allHearts.Count; i++)
        {
            HeartsHealthSystem.Heart heart = allHearts[i];
            createHeartImage(new Vector2(XPos, 0), fullHeartSprite).SetHeartFragments(heart.GetCurrentFragmentAmount());
            XPos += distanceBtwHearts;
        }

        //subscribe to an event
        heartHealthSystem.onDamage += HeartHealthSystem_OnDamaged;
        heartHealthSystem.onHeal += HeartHealthSystem_OnHealed;
        heartHealthSystem.onDead += HeartHealthSystem_OnDead;
    }

    private void HeartHealthSystem_OnDead(object sender, EventArgs e)
    {
        Invoke("onDeath", 0.3f);
    }

    private void HeartHealthSystem_OnHealed(object sender, EventArgs e)
    {
        refreshAllHearts();
    }


    private void HeartHealthSystem_OnDamaged(object sender, EventArgs e) 
    {
        //Transform pos = GameObject.FindWithTag("PlayerCenter").transform;
        refreshAllHearts();
    }

   
    void onDeath()
    {
        Debug.Log("you are dead...");
        SceneManager.LoadScene("Death");
    }


    private void refreshAllHearts()
    {
        // get a lisst of all hearts
        List<HeartsHealthSystem.Heart> hearts = heartHealthSystem.getHeartsList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage img = heartImageList[i];
            HeartsHealthSystem.Heart heart = hearts[i]; // update heart
            img.SetHeartFragments(heart.GetCurrentFragmentAmount()); // reset fragment
        }
    }


    private HeartImage createHeartImage(Vector2 anchorPosition, Sprite heartSprite)
    {
        // create a game object and set parent
        GameObject obj = new GameObject("Heart", typeof(Image));
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
       

        // set rect transform
        obj.GetComponent<RectTransform>().anchoredPosition = anchorPosition;
        obj.GetComponent<RectTransform>().sizeDelta = Vector2.one * sizeDelta;

        // set heart sprite and add it
        obj.GetComponent<Image>().sprite = heartSprite;
        Image img = obj.GetComponent<Image>();
        HeartImage heartImgClass = new HeartImage(img, this);
        heartImageList.Add(heartImgClass);

        return heartImgClass;
    }

    public class HeartImage {

        private Image heartImg;
        private HealthVisual healthVisual;
        public HeartImage(Image img, HealthVisual visual)
        {
            heartImg = img;
            healthVisual = visual;
        }

        public void SetHeartFragments(int frag)
        {
            switch (frag) {
                case 0:
                    heartImg.sprite = healthVisual.emptyheartSprite;
                    break;
                case 1:
                    heartImg.sprite = healthVisual.halfHeartSprite;
                    break;
                case 2:
                    heartImg.sprite = healthVisual.fullHeartSprite;
                    break;

            }

                    
        }

    }

}
