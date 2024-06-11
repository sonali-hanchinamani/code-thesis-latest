using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class tcpserver : MonoBehaviour
{
    public Camera frontViewCamera;
    public Camera leftMirrorCamera;
    public Camera rightMirrorCamera;

    private TcpListener listener;
    private TcpClient client;
    private NetworkStream stream;
    private bool isRunning = true;

    void Start()
    {
        listener = new TcpListener(IPAddress.Any, 1234);
        listener.Start();

        Thread thread = new Thread(new ThreadStart(ListenForClients));
        thread.Start();
    }

    void Update()
    {
        // Update car simulation based on received data
    }

    void ListenForClients()
    {
        try
        {
            client = listener.AcceptTcpClient();
            stream = client.GetStream();

            while (isRunning)
            {
                // Read data from the client
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                
                // Process the received data
            }
        }
        catch (SocketException)
        {
            // Handle socket exception
        }
    }

    void OnDestroy()
    {
        isRunning = false;
        listener.Stop();
        if (client != null)
            client.Close();
    }
}
