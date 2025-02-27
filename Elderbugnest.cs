using System.Collections.Generic;
using Modding;
using UnityEngine;
using Satchel;

namespace Elderbugnest {
    public class Elderbugnest: Mod {
        static GameObject elderbugPrefab;
        new public string GetName() => "Elderbugnest";
        public override string GetVersion() => "v1.0.0.1";
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            On.GameManager.OnNextLevelReady += sceneChangeFunc;
            elderbugPrefab = preloadedObjects["Town"]["_NPCs/Elderbug"];
            Object.DontDestroyOnLoad(elderbugPrefab);
        }

        public override List<(string, string)> GetPreloadNames() {
            return new List<(string, string)> {
                ("Town","_NPCs/Elderbug")
            };
        }

        private void sceneChangeFunc(On.GameManager.orig_OnNextLevelReady orig, GameManager self) {
            orig(self);
            switch(self.sceneName) {
                case "Room_Charm_Shop":
                    placeElderbug(-1, 2.25f, "Charm Slug");
                    break;
                case "Grimm_Divine":
                    placeElderbug(new Vector3(18.87f, 7.33f, 0.009f), -1, 2, "Divine NPC");
                    break;
                case "Room_mapper":
                    placeElderbug(new Vector3(19.64f, 7.35f, 0.195f), -1, 1.25f, "Iselda");
                    break;
                case "Fungus2_26":
                    placeElderbug(new Vector3(46, 5.5f, 0.01f), -1, 1.25f, "Leg Eater");
                    break;
                case "Room_Colosseum_01":
                    GameObject elder = placeElderbug(new Vector3(22.4f,10.93f,0.106f), -1, 1.25f, "Little Fool NPC");
                    elder.transform.Rotate(0, 0, 180);
                    break;
                case "Fungus3_35":
                    placeElderbug(1, 1.25f, "Banker");
                    break;
                case "Room_nailsmith":
                    placeElderbug(new Vector3(17.6834f, 5.7589f, 0.018f), 1, 1.5f, "Nailsmith");
                    break;
                case "Ruins1_05b":
                    placeElderbug(new Vector3(53.3077f, 24.49f, 0.03f), -1, 1.25f, "Relic Dealer");
                    break;
                case "Room_ruinhouse":
                    placeElderbug(new Vector3(15.73f, 6.87f, 0.01f), -1, .7f, "Sly Dazed");
                    break;
                case "Room_shop":
                    placeElderbug(-1, 1.25f, "Sly Shop");
                    break;
                case "Room_Sly_Storeroom":
                    placeElderbug(new Vector3(75.77f, 3.88f, 0.006f), 1, 0.7f, "Sly Basement NPC");
                    break;
                case "Waterways_03":
                    placeElderbug(new Vector3(98.9f,7.03f,0.008f), 1, 3.5f, "Tuk NPC");
                    break;
                case "Room_nailmaster":
                    placeElderbug(new Vector3(68.63f,5.58f,0.01f), -1, 2.2f, "NM Mato NPC");
                    break;
                case "Room_nailmaster_03":
                    placeElderbug(new Vector3(31.01f,5.56f,0.01f),-1, 2.2f, "NM Oro NPC");
                    break;
                case "Room_nailmaster_02":
                    placeElderbug(new Vector3(33.83f,5.54f,0.006f), 1, 2.2f, "NM Sheo NPC");
                    break;
                case "Fungus2_23":
                    placeElderbug(-1, 1.25f, "Bretta Dazed");
                    break;
                case "Grimm_Main_Tent":
                    placeElderbug(new Vector3(51.35f,6.75f,0.006f), 1, 1.55f, "Brum NPC");
                    break;
                case "Room_Mansion":
                    placeElderbug(new Vector3(22.23f,7.68f,0.006f), -1, 2.25f, "Xun NPC");
                    break;
                case "RestingGrounds_07":
                    placeElderbug(1, 1.25f, "Dream Moth");
                    break;
                case "Deepnest_East_04":
                    placeElderbug(-1, 5, "Big Caterpillar");
                    break;
                case "Ruins_House_03":
                    placeElderbug(new Vector3(21.63f,52.36f,0.004f), -1, 1.25f, "Emilitia NPC");
                    break;
                case "GG_Waterways":
                    placeElderbug(-1, 1.25f, "Junk_Fluke_Nervous");
                    break;
                case "Room_GG_Shortcut":
                    placeElderbug(new Vector3(107.33f, 69.49f, 0.006f), -1, 1.25f, "Fluke Hermit");
                    break;
                case "Room_Mask_Maker":
                    placeElderbug(new Vector3(21.15f,7.58f,0.023f), 1, 2, "Maskmaker NPC");
                    GameObject.Find("mm_body").GetComponent<SpriteRenderer>().enabled = false;
                    break;
                case "Room_Queen":
                    placeElderbug(new Vector3(70.5266f, 10.5194f, 0.006f), -1, 3, "Queen");
                    GameObject.Find("queen_0022_a").GetComponent<SpriteRenderer>().enabled = false;
                    GameObject.Find("queen_0023_a").GetComponent<SpriteRenderer>().enabled = false;
                    break;
            }
        }

        private GameObject placeElderbug(int xScale, float scale, string npc) {
            GameObject npcGO = GameObject.Find(npc);
            if(npcGO == null)
                return null;
            return placeElderbug(npcGO.transform.position, xScale, scale, npc);
        }

        private GameObject placeElderbug(Vector3 position, int xScale, float scale, string npc) {
            GameObject npcGO = GameObject.Find(npc);
            if(npcGO == null)
                return null;
            GameObject elderClone = GameObject.Instantiate(elderbugPrefab, position, Quaternion.identity);
            elderClone.transform.SetScaleX(xScale * scale);
            elderClone.transform.SetScaleY(scale);
            for(int i = 0; i < 2; i++)
                elderClone.RemoveComponent<PlayMakerFSM>();
            foreach(string item in new string[] { "Dream Dialogue", "Flower Give", "Dream Dialogue Flower" }) {
                GameObject.Destroy(elderClone.FindGameObjectInChildren(item));
            }
            MeshRenderer mr = npcGO.GetComponent<MeshRenderer>();
            if(mr == null) {
                SpriteRenderer sr = npcGO.GetComponent<SpriteRenderer>();
                elderClone.SetActive(sr.enabled);
                sr.enabled = false;
            }
            else {
                elderClone.SetActive(mr.enabled);
                mr.enabled = false;
            }
            return elderClone;
        }
    }
}
