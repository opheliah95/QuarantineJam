using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

    public Sprite characterImage;

    [TextArea(3,10)]
    public string instructionsFollowed;

    public string animationAssociated;
    public GameObject objectItself;
}
