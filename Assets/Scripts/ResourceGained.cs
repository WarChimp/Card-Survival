using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGained : MonoBehaviour
{

    public static ResourceGained instance;

    [SerializeField] private GameObject _resourcePopupPrefab;
    [SerializeField] private Sprite[] _images;
    [SerializeField] private Image _imageChange;
    [SerializeField] private Text _resourceGained;
    [SerializeField] private Transform _resourceTransform;
    private GameObject rg;
    private bool _readyToSpawn = true;

    private int amountOfPopups = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }


    public void GainResources(int amountGained, int imageToChange)
    {

        amountOfPopups++;
        rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position + new Vector3(0, _resourceTransform.position.y + amountOfPopups * 1f), Quaternion.identity, gameObject.transform);
        rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
        rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "+ " + amountGained.ToString();


        Destroy(rg, 1.3f);
        amountOfPopups--;
    }
    public void GainResources(float amountGained, int imageToChange)
    {

        amountOfPopups++;
        rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position + new Vector3(0, _resourceTransform.position.y + amountOfPopups * 1f), Quaternion.identity, gameObject.transform);
        rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
        rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "+ " + amountGained.ToString();


        Destroy(rg, 1.3f);
        amountOfPopups--;

    }

    public void LoseResource(int amountLost, int imageToChange)
    {
        StartCoroutine(QueuePopupNegative(amountLost, imageToChange));
        //rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position + new Vector3(0, _resourceTransform.position.y - amountOfPopups * 1.5f), Quaternion.identity, gameObject.transform);
        //rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
        //rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "- " + amountLost.ToString();
        //Destroy(rg, 1.3f);
    }
    public void LoseResource(float amountLost, int imageToChange)
    {
        StartCoroutine(QueuePopupNegative(amountLost, imageToChange));

        //rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position + new Vector3(0, _resourceTransform.position.y - amountOfPopups * 1.5f), Quaternion.identity, gameObject.transform);
        //rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
        //rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "- " + amountLost.ToString();
        //Destroy(rg, 1.3f);

    }

    IEnumerator QueuePopup(int amountGained, int imageToChange)
    {
        if(_readyToSpawn == true)
        {
            rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position, Quaternion.identity, gameObject.transform);
            rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
            rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "+ " + amountGained.ToString();
            Destroy(rg, 1.3f);
        }
        
        yield return new WaitForSeconds(0.8f);

        _readyToSpawn = false;
    }
    IEnumerator QueuePopup(float amountGained, int imageToChange)
    {
        if (_readyToSpawn == true)
        {
            rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position, Quaternion.identity, gameObject.transform);
            rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
            rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "+ " + amountGained.ToString();
            Destroy(rg, 1.3f);
        }

        yield return new WaitForSeconds(0.8f);

        _readyToSpawn = false;
    }

    IEnumerator QueuePopupNegative(int amountLost, int imageToChange)
    {
        yield return new WaitForSeconds(0.5f);

        rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position + new Vector3(0, _resourceTransform.position.y + 2f), Quaternion.identity, gameObject.transform);
        rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
        rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "- " + amountLost.ToString();
        Destroy(rg, 1.3f);


    }
    IEnumerator QueuePopupNegative(float amountLost, int imageToChange)
    {
        yield return new WaitForSeconds(0.5f);

        rg = Instantiate(_resourcePopupPrefab, _resourceTransform.position + new Vector3(0, _resourceTransform.position.y + 2f), Quaternion.identity, gameObject.transform);
        rg.transform.Find("Background").transform.Find("Resource_Image").GetComponent<Image>().sprite = _images[imageToChange];
        rg.transform.Find("Background").transform.Find("Resource_Text").GetComponent<Text>().text = "- " + amountLost.ToString();
        Destroy(rg, 1.3f);

    }

}
