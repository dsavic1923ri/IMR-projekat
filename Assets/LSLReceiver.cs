using UnityEngine;
using LSL;

public class LSLReceiver : MonoBehaviour
{
    private StreamInlet inlet;
    private float[] sample; // Removed the fixed '[1]' size so it can resize dynamically

    public float CurrentValue { get; private set; }

    void Update()
    {
        // Look for the stream named "toto" if we haven't found it yet
        if (inlet == null)
        {
            StreamInfo[] results = LSL.LSL.resolve_stream("name", "toto", 1, 0.0f);
            
            if (results.Length > 0)
            {
                inlet = new StreamInlet(results[0]);
                
                // NEW: Ask openViBE exactly how many channels it is sending,
                // and configure our buffer array to match it perfectly!
                int channelCount = inlet.info().channel_count();
                sample = new float[channelCount];
                
                Debug.Log($"Connected to openViBE stream 'toto' with {channelCount} channel(s).");
            }
        }

        // If connected, grab the data safely
        if (inlet != null)
        {
            double timestamp = inlet.pull_sample(sample, 0.0f);
            if (timestamp != 0.0)
            {
                // Assign the first channel to your movement value
                CurrentValue = sample[0]; 
            }
        }
    }
}