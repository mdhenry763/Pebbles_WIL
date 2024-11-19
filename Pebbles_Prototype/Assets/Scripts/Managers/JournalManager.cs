using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public GameManager _GameManager;
    public TMP_Text tutorialConservation, tutorialSource, tutorialTreatment, level1Conservation, level1Source, level1Treatment;
    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameManager.Instance.GetComponent<GameManager>();
    }

    public void UpdateJournalText()
    {
        if (_GameManager.tutorialScore > 10 )
        {
            tutorialSource.text =
                "Dams are giant walls built across rivers. They help collect and store water in huge man made lakes. This water can come from rain, melting snow, or rivers. The water in the dams can be filled with animals, dirt and other things that we don’t want to drink, so the water gets piped to a water treatment plant.";
            if (_GameManager.tutorialScore >= 50 )
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

        if (_GameManager.level1Score > 10)
        {
            level1Source.text = 
                "Clean water is stored in large tanks called reservoirs. These keep the water safe and ready to be sent to our homes. Think of reservoirs as giant water bottles that store clean water until we need it.";
             if(_GameManager.level1Score >= 50)
             {
                 level1Treatment.text =
                     "From a dam, water goes to a water treatment plant. Here, the water is cleaned and purified. The plant removes dirt, germs, and other harmful substances to make the water safe to drink.";
                 if (_GameManager.level1Score >= 80)
                 {
                     level1Conservation.text =
                         "Make sure to water plants early in the morning or late in the evening. Watering your plants during the cooler parts of the day reduces water evaporation. This means more water can reach the roots while using less water! It's a simple way to be more efficient with your garden's water use.";
                 }
             }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
