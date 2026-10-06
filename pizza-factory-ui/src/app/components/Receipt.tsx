"use client";

import { OrderResponse } from "@/lib/types";

interface Props {
    order: OrderResponse;
}

export default function Receipt({ order }: Props) {
    const handlePrint = () => window.print();

    return (
        <div style={{ maxWidth: "560px", margin: "0 auto" }}>
            <div
                className="card"
                style={{
                    background: "linear-gradient(135deg, #18181c, #1f1f26)",
                    border: "1px solid var(--border)",
                    borderTop: "4px solid var(--accent-orange)",
                }}
            >
                {/* Header */}
                <div style={{ textAlign: "center", marginBottom: "1.5rem" }}>
                    <div style={{ fontSize: "2.5rem" }}>🍕</div>
                    <h1
                        style={{
                            fontSize: "1.6rem",
                            fontWeight: 800,
                            background: "linear-gradient(135deg, var(--accent-orange), var(--accent-gold))",
                            WebkitBackgroundClip: "text",
                            WebkitTextFillColor: "transparent",
                            backgroundClip: "text",
                        }}
                    >
                        PIZZA FACTORY
                    </h1>
                    {order.customerName && (
                        <p style={{ color: "var(--text-muted)", marginTop: "0.25rem" }}>
                            Customer: <strong style={{ color: "var(--text-primary)" }}>{order.customerName}</strong>
                        </p>
                    )}
                    <p style={{ fontSize: "0.75rem", color: "var(--text-faint)", marginTop: "0.25rem" }}>
                        Order #{order.orderId.substring(0, 8).toUpperCase()}
                    </p>
                </div>

                <hr className="divider" />

                {/* Pizzas */}
                {order.pizzas.map((pizza, i) => (
                    <div key={i} style={{ marginBottom: "1rem" }}>
                        <div style={{ display: "flex", justifyContent: "space-between", fontWeight: 600 }}>
                            <span>Pizza #{i + 1} — {pizza.size}</span>
                            <span style={{ color: "var(--accent-gold)" }}>${pizza.totalCost.toFixed(2)}</span>
                        </div>
                        <div style={{ marginLeft: "1rem", marginTop: "0.3rem" }}>
                            {pizza.toppings.map((t, j) => (
                                <div
                                    key={j}
                                    style={{
                                        display: "flex",
                                        justifyContent: "space-between",
                                        fontSize: "0.85rem",
                                        color: "var(--text-muted)",
                                        padding: "0.1rem 0",
                                    }}
                                >
                                    <span>• {t.name} ({t.type})</span>
                                    <span>+${t.cost.toFixed(2)}</span>
                                </div>
                            ))}
                            <div style={{ fontSize: "0.8rem", color: "var(--text-faint)", marginTop: "0.2rem" }}>
                                Base: ${pizza.basePrice.toFixed(2)} + {pizza.toppings.length} toppings
                            </div>
                        </div>
                    </div>
                ))}

                <hr className="divider" />

                {/* Totals */}
                <div style={{ display: "flex", flexDirection: "column", gap: "0.4rem" }}>
                    <div style={{ display: "flex", justifyContent: "space-between", color: "var(--text-muted)" }}>
                        <span>Subtotal</span>
                        <span>${order.subtotal.toFixed(2)}</span>
                    </div>
                    {order.couponCode && (
                        <div style={{ display: "flex", justifyContent: "space-between", color: "var(--accent-green)" }}>
                            <span>Coupon ({order.couponCode}) -{order.discountPercent}%</span>
                            <span>-${order.discount.toFixed(2)}</span>
                        </div>
                    )}
                    <div
                        style={{
                            display: "flex",
                            justifyContent: "space-between",
                            fontWeight: 800,
                            fontSize: "1.2rem",
                            marginTop: "0.5rem",
                            paddingTop: "0.5rem",
                            borderTop: "2px dashed var(--border)",
                        }}
                    >
                        <span>TOTAL</span>
                        <span style={{ color: "var(--accent-orange)" }}>${order.total.toFixed(2)}</span>
                    </div>
                </div>

                <hr className="divider" />

                <div style={{ textAlign: "center", color: "var(--text-faint)", fontSize: "0.8rem" }}>
                    <p>Thank you for your order!</p>
                </div>
            </div>

            <div style={{ display: "flex", gap: "1rem", marginTop: "1.5rem", justifyContent: "center" }}>
                <button className="btn btn-primary" onClick={handlePrint}>
                    🖨 Print Receipt
                </button>
                <a href="/" className="btn btn-ghost">
                    ← New Order
                </a>
            </div>
        </div>
    );
}
