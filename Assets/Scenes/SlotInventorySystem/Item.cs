using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName="Inventory/Item")]
public class Item : ScriptableObject
{
	public int ID;
	public string Name;
	public string Description;
	public Sprite Icon;
}
