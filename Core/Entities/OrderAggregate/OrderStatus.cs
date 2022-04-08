using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="در حال بررسی")]
        Pending,
        [EnumMember(Value = "پرداخت موفق")]
        PaymentReceived,
        [EnumMember(Value = "پرداخت ناموفق")]
        PaymentFailed
    }
}
