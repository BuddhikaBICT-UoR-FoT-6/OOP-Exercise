export type PizzaSize = "Small" | "Medium" | "Large";
export type ToppingType = "Cheese" | "Ham" | "Pepperoni";

export interface ToppingRequest {
    type: ToppingType;
    name: string;
}

export interface AddPizzaRequest {
    size: PizzaSize;
    toppings: ToppingRequest[];
}

export interface ApplyCouponRequest {
    couponCode: string;
    discountPercent: number;
}

export interface ToppingResponse {
    name: string;
    type: string;
    cost: number;
}

export interface PizzaResponse {
    size: string;
    toppings: ToppingResponse[];
    basePrice: number;
    toppingsCost: number;
    totalCost: number;
    description: string;
}

export interface OrderResponse {
    orderId: string;
    customerName?: string;
    pizzas: PizzaResponse[];
    subtotal: number;
    couponCode?: string;
    discountPercent: number;
    discount: number;
    total: number;
    receipt: string;
}

export interface PresetTopping {
    type: string;
    name: string;
}

export interface Preset {
    name: string;
    toppings: PresetTopping[];
}
