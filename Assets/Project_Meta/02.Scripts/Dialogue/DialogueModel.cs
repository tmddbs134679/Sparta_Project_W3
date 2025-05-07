using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.Networking;
using System.Net.NetworkInformation;


[System.Serializable]
public class NPCDialogue
{
    public string NPC_ID;
    public string NPC_Name;
    public bool HasOptions;
    public string NPC_Image;
    public string Dialogue_ID;
    public string State;
    public int Sequence;
    public string Text;
    public List<string> PlayerChoices;
    public List<string> NextDialogueIDs;



    public class DialogueModel
    {
        private Dictionary<string, List<NPCDialogue>> dialogues = new Dictionary<string, List<NPCDialogue>>();
        private string csvFileName = "Dialogue.csv";
        private string csvPath;

        public IEnumerator LoadCSV()
        {
            csvPath = Path.Combine(Application.streamingAssetsPath, csvFileName);


            if (File.Exists(csvPath))
            {
                string fileData = File.ReadAllText(csvPath);
                yield return ParseCSV(fileData);
            }
            else
            {
                Debug.LogWarning($"CSV 파일이 {csvPath} 에 없음. Resources 폴더에서 로드");
                TextAsset csvFile = Resources.Load<TextAsset>(csvFileName.Replace(".csv", ""));
                if (csvFile != null)
                {
                    yield return ParseCSV(csvFile.text);
                }
                else
                {
                    Debug.LogError("CSV 파일을 찾을 수 없음.");
                }
            }
        }


        private IEnumerator ParseCSV(string csvText)
        {
            string[] lines = csvText.Split('\n');
            if (lines.Length < 2) yield break;

            string[] headers = lines[0].Split(',');
            Dictionary<string, int> columnIndex = new Dictionary<string, int>();

            for (int i = 0; i < headers.Length; i++)
            {
                columnIndex[headers[i].Trim()] = i;
            }

            dialogues.Clear(); // 기존 데이터 초기화

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = ParseCSVLine(lines[i]);
                if (values.Length < headers.Length) continue;

                NPCDialogue dialogue = new NPCDialogue()
                {
                    NPC_ID = values[columnIndex["NPC_ID"]].Trim(),
                    NPC_Name = values[columnIndex["NPC_Name"]].Trim(),
                    HasOptions = values[columnIndex["HasOptions"]].Trim() == "TRUE",
                    NPC_Image = values[columnIndex["NPC_Image"]].Trim(),
                    Dialogue_ID = values[columnIndex["Dialogue_ID"]].Trim(),
                    State = values[columnIndex["State"]].Trim(),
                    Sequence = int.Parse(values[columnIndex["Sequence"]].Trim()),
                    Text = values[columnIndex["Text"]].Trim(),
                    PlayerChoices = new List<string>(),
                    NextDialogueIDs = new List<string>()
                };

 
                string playerChoicesRaw = values[columnIndex["Player"]].Trim();
                if (!string.IsNullOrEmpty(playerChoicesRaw))
                {
                    dialogue.PlayerChoices = playerChoicesRaw.Split(';').Select(p => p.Trim()).ToList();
                }

                string nextDialogueIDsRaw = values[columnIndex["NextDialogueID"]].Trim();
                if (!string.IsNullOrEmpty(nextDialogueIDsRaw))
                {
                    dialogue.NextDialogueIDs = nextDialogueIDsRaw.Split(';').Select(id => id.Trim()).ToList();
                }

                if (!dialogues.ContainsKey(dialogue.NPC_ID))
                {
                    dialogues[dialogue.NPC_ID] = new List<NPCDialogue>();
                }

                dialogues[dialogue.NPC_ID].Add(dialogue);

                if (i % 10 == 0) yield return null;
            }

            Debug.Log("CSV 데이터 로딩 완료");
        }

        private string[] ParseCSVLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            string currentField = "";

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }
            fields.Add(currentField);

            for (int i = 0; i < fields.Count; i++)
            {
                fields[i] = fields[i].Trim();
                if (fields[i].StartsWith("\"") && fields[i].EndsWith("\""))
                {
                    fields[i] = fields[i].Substring(1, fields[i].Length - 2).Replace("\"\"", "\"");
                }
            }

            return fields.ToArray();
        }

        public List<NPCDialogue> GetDialogues(string npcID) //NPC에 모든 대화 가지고오기
        {
            if (dialogues.ContainsKey(npcID))
            {
                return dialogues[npcID];
            }
            return new List<NPCDialogue>();
        }

        public List<NPCDialogue> GetRandomDialogues(string npcID) //NPC에 모든 대화 가지고오기
        {
            if (!dialogues.ContainsKey(npcID)) return new List<NPCDialogue>();

            // 인덱스가 0인 대화들만 필터링
            List<NPCDialogue> randomDialogues = dialogues[npcID]
                .Where(d => d.Sequence == 0)
                .ToList();

            return randomDialogues;
        }

        public NPCDialogue GetDialogueByID(string dialogueID)
        {
            foreach (var npcDialogues in dialogues.Values)
            {
                NPCDialogue dialogue = npcDialogues.Find(d => d.Dialogue_ID == dialogueID);
                if (dialogue != null)
                {
                    return dialogue;
                }
            }
            return null;
        }


        public List<NPCDialogue> GetDialogues(string npcID, ENPCState state) //NPC 상태에 따른 대화 가지고 오기
        {
            if (dialogues.ContainsKey(npcID))
            {

                return dialogues[npcID]
                    .Where(d => d.State == state.ToString()) // 현재 설정된 언어만 필터링
                    .ToList();
            }
            return new List<NPCDialogue>();
        }

        public NPCDialogue GetCurrentDialogue(string npcId, int currentIndex, ENPCState state)
        {
            List<NPCDialogue> dialogues = GetDialogues(npcId, state);

            if (currentIndex >= 0 && currentIndex < dialogues.Count)
            {
                return dialogues[currentIndex];
            }
            return null;
        }


    }
}
