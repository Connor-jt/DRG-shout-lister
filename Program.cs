using System.IO;

namespace DRG_shout_lister
{
    internal class Program
    {


        static string[] folders_to_fetch_from = new string[]{
            "test",
        };

        static string source_folder = "D:\\_DRG modding\\FSD-Template-main\\Templates\\DRG unpacked\\FSD\\Content"; // this is just used to substring length off of each found asset file

        static string new_content_folder = "C:\\Users\\Joe bingle\\Downloads\\DRG output\\Content";
        static string new_content_csv = "C:\\Users\\Joe bingle\\Downloads\\DRG output\\assets.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string[] files = Directory.GetFiles(source_folder, "shout_*", SearchOption.AllDirectories); // im pretty sure windows caches this search, so running multiple tests is faster after the first run

            int row_index = 1;

            using (FileStream fs = File.OpenWrite(new_content_csv))
            using (StreamWriter sw = new(fs)){
                sw.WriteLine("Name,Shout");

                for (int i = 0; i < files.Length; i++){
                    string file = files[i];

                    string relative_path = file.Substring(source_folder.Length); // this will probably inherit the slash not in the source
                    string new_path = new_content_folder + relative_path;

                    //new FileInfo(new_path).Directory.Create();
                    //File.Copy(file, new_path);

                    // generate the name of the uasset file (theres duplicate files with the same name but different extensions)
                    if (Path.GetExtension(relative_path) == ".uasset"){
                        string classname = Path.GetFileNameWithoutExtension(relative_path);
                        sw.WriteLine(row_index++ + ",\"DialogDataAsset'/Game" + relative_path.Split(".").FirstOrDefault().Replace("\\", "/") + "." + classname + "'\"");
                    }

                }
            }


        }
    }
}