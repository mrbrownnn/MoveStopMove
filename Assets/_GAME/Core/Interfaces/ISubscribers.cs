public interface ISubscribers //Dùng Interface này để thực hiện bắt đầu và kết thúc Level. Những class thực thi sẽ override các hàm ở dưới để thực hiện 
{
    void SubscribeEvent(); 
    void UnsubscribeEvent();
}