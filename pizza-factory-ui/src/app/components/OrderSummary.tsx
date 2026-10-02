"use client";

import { OrderResponse } from "@/lib/types";

interface Props {
    order: OrderResponse | null;
}

export default function OrderSummary({ order }: Props) {
    if (!order || order.pizzas.length === 0) {
        return (
            <div
                className="card"
                style={{ textAlign: "center", padding: "3rem 1.5rem", color: "var(--text-faint)" }}
            >
                <div style={{ fontSize: "3rem", marginBottom: "0.5rem" }}>🛒</div>
                <p>Your order is empty.</p>
                <p style={{ fontSize: "0.8rem", marginTop: "0.25rem" }}>Add a pizza to get started!</p>
            </div>
        );
    }

    return (
        <div className="card" style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
            <h2 style={{ fontSize: "1.1rem", fontWeight: 700 }}>
                🧾 Order Summary{" "}
                <span className="badge">{order.pizzas.length} pizza{order.pizzas.length > 1 ? "s" : ""}</span>
            </h2>

            {order.pizzas.map((pizza, i) => (
                <div
                    key={i}
                    style={{
                        padding: "0.85rem 1rem",
                        borderRadius: "var(--radius-md)",
                        background: "var(--bg-dark)",
                        border: "1px solid var(--border)",
                    }}
                >
                    <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "0.4rem" }}>
                        <span style={{ fontWeight: 600 }}>🍕 {pizza.size} Pizza</span>
                        <span style={{ color: "var(--accent-gold)", fontWeight: 700 }}>
                            ${pizza.totalCost.toFixed(2)}
                        </span>
                    </div>
                    <div style={{ display: "flex", flexWrap: "wrap", gap: "0.3rem" }}>
                        {pizza.toppings.length === 0 ? (
                            <span style={{ fontSize: "0.8rem", color: "var(--text-faint)" }}>No toppings</span>
                        ) : (
                            pizza.toppings.map((t, j) => (
                                <span key={j} className={`chip chip-${t.type.toLowerCase()}`}>
                                    {t.name}
                                </span>
                            ))
                        )}
                    </div>
                    <div style={{ fontSize: "0.75rem", color: "var(--text-muted)", marginTop: "0.4rem" }}>
                        Base ${pizza.basePrice.toFixed(2)} + {pizza.toppings.length} × $2.00 toppings
                    </div>
                </div>
            ))}

            <hr className="divider" />

            <div style={{ display: "flex", flexDirection: "column", gap: "0.3rem", fontSize: "0.9rem" }}>
                <div style={{ display: "flex", justifyContent: "space-between" }}>
                    <span style={{ color: "var(--text-muted)" }}>Subtotal</span>
                    <span>${order.subtotal.toFixed(2)}</span>
                </div>
                {order.couponCode && (
                    <div style={{ display: "flex", justifyContent: "space-between", color: "var(--accent-green)" }}>
                        <span>Coupon ({order.couponCode}, -{order.discountPercent}%)</span>
                        <span>-${order.discount.toFixed(2)}</span>
                    </div>
                )}
                <div
                    style={{
                        display: "flex",
                        justifyContent: "space-between",
                        fontWeight: 700,
                        fontSize: "1.05rem",
                        marginTop: "0.3rem",
                    }}
                >
                    <span>Total</span>
                    <span style={{ color: "var(--accent-orange)" }}>${order.total.toFixed(2)}</span>
                </div>
            </div>
        </div>
    );
}
