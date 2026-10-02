import {
    AddPizzaRequest,
    ApplyCouponRequest,
    OrderResponse,
    Preset,
} from "./types";

const BASE_URL = "http://localhost:5000/api";

// Handles API responses and throws an error if the response is not OK.
async function handleResponse<T>(res: Response): Promise<T> {
    if (!res.ok) {
        // Catches any JSON parsing errors.
        const error = await res.json().catch(() => ({ message: res.statusText }));
        throw new Error(error.message ?? "API request failed");
    }
    if (res.status === 204) return undefined as T;
    return res.json() as Promise<T>;
}

export async function createOrder(customerName?: string): Promise<{ orderId: string }> {
    const res = await fetch(`${BASE_URL}/orders`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ customerName: customerName ?? null }),
    });
    return handleResponse(res);
}

export async function addPizza(orderId: string, req: AddPizzaRequest): Promise<void> {
    const res = await fetch(`${BASE_URL}/orders/${orderId}/pizzas`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(req),
    });
    return handleResponse(res);
}

export async function applyCoupon(orderId: string, req: ApplyCouponRequest): Promise<void> {
    const res = await fetch(`${BASE_URL}/orders/${orderId}/coupon`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(req),
    });
    return handleResponse(res);
}

export async function getOrder(orderId: string): Promise<OrderResponse> {
    const res = await fetch(`${BASE_URL}/orders/${orderId}`);
    return handleResponse(res);
}

export async function checkout(orderId: string): Promise<OrderResponse> {
    const res = await fetch(`${BASE_URL}/orders/${orderId}/checkout`);
    return handleResponse(res);
}

export async function getSizes(): Promise<string[]> {
    const res = await fetch(`${BASE_URL}/meta/sizes`);
    return handleResponse(res);
}

export async function getToppingTypes(): Promise<string[]> {
    const res = await fetch(`${BASE_URL}/meta/toppings`);
    return handleResponse(res);
}

export async function getPresets(): Promise<Preset[]> {
    const res = await fetch(`${BASE_URL}/meta/presets`);
    return handleResponse(res);
}
