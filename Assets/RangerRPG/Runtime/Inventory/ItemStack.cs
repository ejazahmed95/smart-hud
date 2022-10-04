
public class ItemStack
{
    public ItemData data { get; private set; }
    public int stackSize { get; private set; }

    public ItemStack(ItemData i_itemData)
    {
        this.data = i_itemData;
        AddToStack(i_itemData);
    }

    public void AddToStack(ItemData i_itemData)
    {
        //data.tab.AddToTab(i_itemData);
        stackSize++;
    }

    public void RemoveFromStack()
    {
        // data.tab.RemoveFromTab(data);
        stackSize--;
    }
}
