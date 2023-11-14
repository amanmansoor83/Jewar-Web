using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

class AmazonS3SDK
{
    private const string BUCKET_NAME = "cdn.Broadway.pk";
    private const string S3_KEY = @"galley/";


    /*
    static void Main(string[] args)
    {
        AmazonS3 s3Client = GetS3Client();
        //CreateBucket(s3Client);
        //CreateNewFile(s3Client);
        CreateNewFolder(s3Client);
        CreateNewFileInFolder(s3Client);
    }

    */
    public static bool UploadFileToS3(string folder, string uploadAsFileName, Stream ImageStream, string toWhichBucketName, AmazonS3 client)
    {
        try
        {
            PutObjectRequest request = new PutObjectRequest();
            request.WithKey(folder + uploadAsFileName);
            request.WithInputStream(ImageStream);

            request.WithBucketName(toWhichBucketName);

            client.PutObject(request);
            client.Dispose();
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// Create AmazonS3 from key in Application Config.
    /// </summary>
    /// <returns s3Client></returns>
    public static AmazonS3 GetS3Client()
    {

        AmazonS3Config S3Config = new AmazonS3Config()
        {
            ServiceURL = "s3-ap-southeast-1.amazonaws.com",
            CommunicationProtocol = Amazon.S3.Model.Protocol.HTTP,
        };


        NameValueCollection appConfig = ConfigurationManager.AppSettings;

        AmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(
                appConfig["AWSAccessKey"],
                appConfig["AWSSecretKey"],
                S3Config
                );
        return s3Client;
    }

    /// <summary>
    /// Creates bucket if it is not exists.
    /// </summary>
    /// <param name="client"></param>
    public static void CreateBucket(AmazonS3 client)
    {
        Console.Out.WriteLine("Checking S3 bucket with name " + BUCKET_NAME);

        ListBucketsResponse response = client.ListBuckets();

        bool found = false;
        foreach (S3Bucket bucket in response.Buckets)
        {
            if (bucket.BucketName == BUCKET_NAME)
            {
                Console.Out.WriteLine("   Bucket found will not create it.");
                found = true;
                break;
            }
        }

        if (found == false)
        {
            Console.Out.WriteLine("   Bucket not found will create it.");

            client.PutBucket(new PutBucketRequest().WithBucketName(BUCKET_NAME));

            Console.Out.WriteLine("Created S3 bucket with name " + BUCKET_NAME);
        }
    }

    public static void CreateNewFile(AmazonS3 client)
    {
        String S3_KEY = "Demo Create File.txt";
        PutObjectRequest request = new PutObjectRequest();
        request.WithBucketName(BUCKET_NAME);
        request.WithKey(S3_KEY);
        request.WithContentBody("This is body of S3 object.");
        client.PutObject(request);
    }

    public static void CreateNewFolder(AmazonS3 client)
    {
        String S3_KEY = "Junaid/";
        PutObjectRequest request = new PutObjectRequest();
        request.WithBucketName(BUCKET_NAME);
        request.WithKey(S3_KEY);
        request.WithContentBody("");
        client.PutObject(request);
    }

    public static void CreateNewFileInFolder(AmazonS3 client)
    {
        String S3_KEY = "Demo Create Folder/" + "Demo Create File.txt";
        PutObjectRequest request = new PutObjectRequest();
        request.WithBucketName(BUCKET_NAME);
        request.WithKey(S3_KEY);
        request.WithContentBody("This is body of S3 object.");
        client.PutObject(request);
    }

    public static void UploadFile(AmazonS3 client)
    {
        //S3_KEY is name of file we want upload
        PutObjectRequest request = new PutObjectRequest();
        request.WithBucketName(BUCKET_NAME);
        request.WithKey(S3_KEY);
        //request.WithInputStream(MemoryStream);
        request.WithFilePath("");
        client.PutObject(request);
    }

    public static MemoryStream GetFile(AmazonS3 s3Client)
    {
        using (s3Client)
        {
            MemoryStream file = new MemoryStream();
            try
            {
                GetObjectResponse r = s3Client.GetObject(new GetObjectRequest()
                {
                    BucketName = BUCKET_NAME,
                    Key = S3_KEY
                });
                try
                {
                    long transferred = 0L;
                    BufferedStream stream2 = new BufferedStream(r.ResponseStream);
                    byte[] buffer = new byte[0x2000];
                    int count = 0;
                    while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        file.Write(buffer, 0, count);
                    }
                }
                finally
                {
                }
                return file;
            }
            catch (AmazonS3Exception)
            {
                //Show exception
            }
        }
        return null;
    }

    public static void DeleteFile(AmazonS3 Client, string myKey, string FileNameWithExt)
    {
        DeleteObjectRequest request = new DeleteObjectRequest()
        {
            BucketName = BUCKET_NAME,
            Key = myKey + FileNameWithExt
        };
        S3Response response = Client.DeleteObject(request);
    }

    public static void CopyFile(AmazonS3 s3Client)
    {
        String destinationPath = "";
        CopyObjectRequest request = new CopyObjectRequest()
        {
            SourceBucket = BUCKET_NAME,
            SourceKey = S3_KEY,
            DestinationBucket = BUCKET_NAME,
            DestinationKey = destinationPath
        };
        CopyObjectResponse response = s3Client.CopyObject(request);
    }

    public static void ShareFile(AmazonS3 s3Client)
    {
        S3Response response1 = s3Client.SetACL(new SetACLRequest()
        {
            CannedACL = S3CannedACL.PublicRead,
            BucketName = BUCKET_NAME,
            Key = S3_KEY
        });
    }

    public static String MakeUrl(AmazonS3 s3Client)
    {
        string preSignedURL = s3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
        {
            BucketName = BUCKET_NAME,
            Key = S3_KEY,
            Expires = System.DateTime.Now.AddMinutes(30)

        });

        return preSignedURL;
    }
}