"use client";

import { useState } from "react";
import { applyCoupon } from "@/lib/api";

interface Props {
    orderId: string;
    onApplied: () => void;
}

export default function CouponInput({ orderId, onApplied }: Props) {
    const [code, setCode] = useState("");
    const [discountPercent, setDiscountPercent] = useState<number>(10);
    const [isApplied, setIsApplied] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    const handleApply = async () => {
        if (!code.trim()) { setError("Enter a coupon code."); return; }
        setError(null);
        setIsLoading(true);
        try {
            await applyCoupon(orderId, { couponCode: code.trim(), discountPercent });
            setIsApplied(true);
            onApplied();
        } catch (e: unknown) {
            setError((e as Error).message);
        } finally {
            setIsLoading(false);
        }
    };

    if (isApplied) {
        return (
            <div
                className="card"
                style={{
                    display: "flex",
                    alignItems: "center",
                    gap: "0.75rem",
                    color: "var(--accent-green)",
                    padding: "0.9rem 1.25rem",
                }}
            >
                <span style={{ fontSize: "1.2rem" }}>✅</span>
                <span style={{ fontWeight: 600 }}>
                    Coupon <strong>{code.toUpperCase()}</strong> applied — {discountPercent}% off!
                </span>
            </div>
        );
    }

    return (
        <div className="card" style={{ display: "flex", flexDirection: "column", gap: "0.75rem" }}>
            <span className="label">🎟 Apply Coupon</span>
            <div style={{ display: "flex", gap: "0.5rem" }}>
                <input
                    className="input"
                    placeholder="Coupon code (e.g. SAVE10)"
                    value={code}
                    onChange={(e) => setCode(e.target.value)}
                    style={{ textTransform: "uppercase" }}
                />
                <input
                    className="input"
                    type="number"
                    min={1}
                    max={100}
                    value={discountPercent}
                    onChange={(e) => setDiscountPercent(Number(e.target.value))}
                    style={{ width: "90px" }}
                    title="Discount %"
                />
                <button className="btn btn-primary" onClick={handleApply} disabled={isLoading}>
                    {isLoading ? "..." : "Apply"}
                </button>
            </div>
            {error && <p style={{ color: "var(--accent-red)", fontSize: "0.82rem" }}>⚠ {error}</p>}
            <p style={{ fontSize: "0.78rem", color: "var(--text-faint)" }}>
                Hint: any code works — just set your discount %
            </p>
        </div>
    );
}
