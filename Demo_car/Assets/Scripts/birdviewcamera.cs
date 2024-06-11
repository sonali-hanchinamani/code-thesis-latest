using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace carstimulator{
public class birdviewcamera : MonoBehaviour
{
    public int port = 1234; // Port number used by the server
    
    private TcpListener listener;
    private TcpClient client;
    private NetworkStream stream;
    private bool isRunning = true;
    public bool startcars = false;
   
    public Transform[] targetTransforms; 

    async void Start()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        listener = new TcpListener(ip, port);
        listener.Start();
        await AcceptClientsAsync();
    }

    async Task AcceptClientsAsync()
    {
        try
        {
            client = await listener.AcceptTcpClientAsync();
            stream = client.GetStream();
            Debug.Log("Client connected");
            while (isRunning)
            {
                startcars = true;
                StringBuilder positionDataBuilder = new StringBuilder();
                    foreach (Transform targetTransform in targetTransforms)
                    {
                        positionDataBuilder.AppendFormat("{0},{1},{2},{3},{4},{5},{6}|",
                            targetTransform.name, // Add the name of the game object
                            targetTransform.position.x,
                            targetTransform.position.y,
                            targetTransform.position.z,
                            targetTransform.rotation.eulerAngles.x,
                            targetTransform.rotation.eulerAngles.y,
                            targetTransform.rotation.eulerAngles.z);
                    }
                    //foreach (Transform targetTransform in targetTransforms)
                    //{
                    //    positionDataBuilder.AppendFormat("{0},{1},{2}|", targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
                    //}
                    string positionData = positionDataBuilder.ToString();
                byte[] bytesToSend = Encoding.ASCII.GetBytes(positionData);
                await stream.WriteAsync(bytesToSend, 0, bytesToSend.Length);

                await Task.Delay(10);
            }
        }
        catch (SocketException)
        {
            Debug.Log("Client is not connected yet");
        }
    }

    void OnDestroy()
    {
        startcars = false;
        isRunning = false;
        listener.Stop();
        if (client != null)
            client.Close();
    }
}
}
