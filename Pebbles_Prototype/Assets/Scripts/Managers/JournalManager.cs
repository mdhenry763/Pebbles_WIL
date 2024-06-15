using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;

public class JournalManager : MonoBehaviour
{
    public GameManager _GameManager;
    public TMP_Text tutorialConservation, tutorialSource, tutorialTreatment;
    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameManager.Instance.GetComponent<GameManager>();
    }

    public void UpdateJournalText()
    {
        if (_GameManager.tutorialScore > 10 && _GameManager.tutorialScore < 50)
        {
            tutorialSource.text =
                "Dams are giant walls built across rivers. They help collect and store water in huge man made lakes. This water can come from rain, melting snow, or rivers. The water in the dams can be filled with animals, dirt and other things that we don’t want to drink, so the water gets piped to a water treatment plant.";
            if (_GameManager.tutorialScore >= 50 && _GameManager.tutorialScore < 80)
            {
                tutorialTreatment.text = 
                    "Filtration is one of the last stages of water treatment. The water is passed through many layers of different types of filters. These can be things like sand, gravel and charcoal. The small sizes of the pores can remove even microscopic particles like dust, parasites, bacteria, viruses and chemicals.";
                if (_GameManager.tutorialScore >= 80)
                {
                    tutorialConservation.text = 
                        "Fix leaks as soon as you notice them, a leaky tap or pipe can waste a lot of water over time. Regularly check for leaks and fix them quickly. Most leaky taps can be fixed easily by replacing a washer which is not that difficult to learn. Remember, even small repairs can save a lot of water.";
                }
            }
           
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
