using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Nutty : MonoBehaviour
{
    public Material dissolveMaterial;
    public float dissolveSpeed;
    public float trailDelayTime = 0.3f;
    public VisualEffect vfx;
    public GameObject trailRenderer;
    Material matInstance;
    Animator anim;

    private void Start()
    {
        matInstance = Instantiate<Material>(dissolveMaterial);
        matInstance.SetFloat("_AlphaClipThreshold", 0.15f);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetBool("isDie", true);
    }

    void Die()
    {
        RecursiveReplaceMaterial(gameObject);
        StartCoroutine(DissolveObject());
        vfx.SendEvent("OnDie");
    }

    void RecursiveReplaceMaterial(GameObject go)
    {
        var meshR = go.GetComponent<MeshRenderer>();

        if (meshR != null)
        {
            if (meshR.materials.Length > 1)
            {
                var newMaterials = new Material[meshR.materials.Length];
                for (var i = 0; i < newMaterials.Length; i++)
                    newMaterials[i] = matInstance;

                meshR.materials = newMaterials;
            }
            else
                meshR.material = matInstance;
        }

        if (go.transform.childCount != 0)
            for (int i = 0; i < go.transform.childCount; i++)
                RecursiveReplaceMaterial(go.transform.GetChild(i).gameObject);
    }

    IEnumerator DissolveObject()
    {
        bool isEnded = false;
        float x = 0;

        while (x < 1)
        {
            x += Time.deltaTime / dissolveSpeed;
            matInstance.SetFloat("_AlphaClipThreshold", x);

            if (!isEnded && x > 0.8f)
            {
                isEnded = true;
                vfx.SendEvent("Finish");
            }

            yield return null;
        }

        yield return new WaitForSeconds(trailDelayTime);

        trailRenderer.SetActive(true);
    }
}
