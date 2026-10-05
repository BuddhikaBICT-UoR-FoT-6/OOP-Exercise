"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { createOrder, getOrder } from "@/lib/api";
import { OrderResponse } from "@/lib/types";
import PizzaBuilderForm from "./components/PizzaBuilderForm";
import OrderSummary from "./components/OrderSummary";
import CouponInput from "./components/CouponInput";

export default function HomePage() {
  const router = useRouter();
  const [orderId, setOrderId] = useState<string | null>(null);
  const [customerName, setCustomerName] = useState("");
  const [nameConfirmed, setNameConfirmed] = useState(false);
  const [order, setOrder] = useState<OrderResponse | null>(null);
  const [isCheckingOut, setIsCheckingOut] = useState(false);

  const startOrder = async () => {
    const data = await createOrder(customerName || undefined);
    setOrderId(data.orderId);
    setNameConfirmed(true);
  };

  const refreshOrder = async () => {
    if (!orderId) return;
    const data = await getOrder(orderId);
    setOrder(data);
  };

  const handleCheckout = () => {
    if (!orderId) return;
    setIsCheckingOut(true);
    router.push(`/order/${orderId}`);
  };

  // ── Name Entry Screen ──────────────────────────────────────────────────
  if (!nameConfirmed) {
    return (
      <div
        style={{
          minHeight: "calc(100vh - 60px)",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          padding: "2rem",
        }}
      >
        <div className="card" style={{ maxWidth: "400px", width: "100%", textAlign: "center" }}>
          <div style={{ fontSize: "3.5rem", marginBottom: "0.75rem" }}>🍕</div>
          <h1 style={{ fontSize: "1.5rem", fontWeight: 800, marginBottom: "0.5rem" }}>
            Welcome to Pizza Factory
          </h1>
          <p style={{ color: "var(--text-muted)", marginBottom: "1.5rem", fontSize: "0.9rem" }}>
            Build your perfect pizza order with our interactive factory.
          </p>
          <input
            className="input"
            placeholder="Your name (optional)"
            value={customerName}
            onChange={(e) => setCustomerName(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && startOrder()}
            style={{ marginBottom: "1rem" }}
          />
          <button className="btn btn-primary" style={{ width: "100%" }} onClick={startOrder}>
            Start Ordering 🍕
          </button>
        </div>
      </div>
    );
  }

  // ── Main Order Screen ──────────────────────────────────────────────────
  return (
    <div style={{ padding: "2rem", maxWidth: "1200px", margin: "0 auto" }}>
      {customerName && (
        <p style={{ color: "var(--text-muted)", marginBottom: "1.5rem", fontSize: "0.9rem" }}>
          👋 Hey, <strong style={{ color: "var(--text-primary)" }}>{customerName}</strong>! Build your order below.
        </p>
      )}

      <div
        style={{
          display: "grid",
          gridTemplateColumns: "1fr 380px",
          gap: "1.5rem",
          alignItems: "start",
        }}
      >
        {/* Left: Builder */}
        <div style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
          <PizzaBuilderForm orderId={orderId!} onPizzaAdded={refreshOrder} />
          <CouponInput orderId={orderId!} onApplied={refreshOrder} />
        </div>

        {/* Right: Summary */}
        <div style={{ display: "flex", flexDirection: "column", gap: "1rem" }}>
          <OrderSummary order={order} />
          {order && order.pizzas.length > 0 && (
            <button
              className="btn btn-primary"
              style={{ width: "100%", justifyContent: "center", padding: "0.85rem" }}
              onClick={handleCheckout}
              disabled={isCheckingOut}
            >
              {isCheckingOut ? "Going to checkout..." : "🧾 Checkout & Print Receipt"}
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
