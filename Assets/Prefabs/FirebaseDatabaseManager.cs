using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;


    private void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        WriteDatabase("123", "chao ban");
    }
    public void WriteDatabase(string id, string message)
    {
        reference.Child("Users").Child(id).SetValueAsync(message).ContinueWithOnMainThread(Task =>
        {
            if (Task.IsCompleted)
            {
                Debug.Log("ghi du lieu thanh cong");
            }
            else
            {
                Debug.Log("ghi du lieu that bai: " + Task.Exception);
            }
        });
    }
    public void ReadDatabase(string id)
    {

    }
}
