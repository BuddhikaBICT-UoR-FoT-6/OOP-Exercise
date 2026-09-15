namespace PizzaFactory.API.DTOs.Requests;

// used to send the coupon code and discount percentage to the ApplyCoupon method
public record ApplyCouponRequest(string CouponCode, decimal DiscountPercent);
