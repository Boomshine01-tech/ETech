using ETechEnergie.Shared.Models;

namespace ETechEnergie.Client.Services;

public class CartService
{
    private List<CartItem> _items = new();
    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public int ItemCount => _items.Sum(i => i.Quantity);

    public decimal Total => _items.Sum(i => i.Total);

    public void AddToCart(Product product, int quantity = 1)
    {
        var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
        
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity,
                ImageUrl = product.ImageUrl
            });
        }

        NotifyStateChanged();
    }

    public void RemoveFromCart(int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            _items.Remove(item);
            NotifyStateChanged();
        }
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            if (quantity <= 0)
                RemoveFromCart(productId);
            else
            {
                item.Quantity = quantity;
                NotifyStateChanged();
            }
        }
    }

    public void ClearCart()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
