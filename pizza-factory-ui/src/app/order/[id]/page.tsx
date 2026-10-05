"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import { checkout } from "@/lib/api";
import { OrderResponse } from "@/lib/types";
import Receipt from "@/app/components/Receipt";

export default function OrderPage() {
    const params = useParams<{ id: string }>();
    const [order, setOrder] = useState<OrderResponse | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!params.id) return;
        checkout(params.id)
            .then(setOrder)
            .catch((e: Error) => setError(e.message));
    }, [params.id]);

    if (error) {
        return (
            <div style={{ padding: "2rem", textAlign: "center", color: "var(--accent-red)" }}>
                <p>❌ {error}</p>
                <a href="/" className="btn btn-ghost" style={{ marginTop: "1rem", display: "inline-flex" }}>
                    ← Back
                </a>
            </div>
        );
    }

    if (!order) {
        return (
            <div style={{ padding: "4rem", textAlign: "center", color: "var(--text-muted)" }}>
                <div
                    style={{
                        width: "40px",
                        height: "40px",
                        border: "3px solid var(--border)",
                        borderTopColor: "var(--accent-orange)",
                        borderRadius: "50%",
                        animation: "spin 0.8s linear infinite",
                        margin: "0 auto 1rem",
                    }}
                />
                <style>{`@keyframes spin { to { transform: rotate(360deg); } }`}</style>
                <p>Loading your receipt...</p>
            </div>
        );
    }

    return (
        <div style={{ padding: "2rem" }}>
            <Receipt order={order} />
        </div>
    );
}
