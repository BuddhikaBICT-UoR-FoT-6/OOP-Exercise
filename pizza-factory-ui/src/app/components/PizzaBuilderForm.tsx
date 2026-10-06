"use client";

import { useState } from "react";
import { addPizza } from "@/lib/api";
import { PizzaSize, ToppingRequest, Preset } from "@/lib/types";
import SizeSelector from "./SizeSelector";
import ToppingSelector from "./ToppingSelector";
import PresetSelector from "./PresetSelector";

interface Props {
    orderId: string;
    onPizzaAdded: () => void;
    onOrderExpired: () => void;
}

export default function PizzaBuilderForm({ orderId, onPizzaAdded, onOrderExpired }: Props) {
    const [size, setSize] = useState<PizzaSize | null>(null);
    const [toppings, setToppings] = useState<ToppingRequest[]>([]);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handlePreset = (preset: Preset) => {
        setToppings(
            preset.toppings.map((t) => ({
                type: t.type as ToppingRequest["type"],
                name: t.name,
            }))
        );
    };

    const handleSubmit = async () => {
        if (!size) { setError("Please select a size."); return; }
        setError(null);
        setIsSubmitting(true);
        try {
            await addPizza(orderId, { size, toppings });
            setSize(null);
            setToppings([]);
            onPizzaAdded();
        } catch (e: unknown) {
            const msg = (e as Error).message;
            // Order was wiped by an API restart — silently re-create it
            if (msg.includes("404") || msg.toLowerCase().includes("not found")) {
                onOrderExpired();
            } else {
                setError(msg);
            }
        } finally {
            setIsSubmitting(false);
        }
    };

    const cost = size
        ? (size === "Small" ? 10 : size === "Medium" ? 12 : 14) + toppings.length * 2
        : 0;

    return (
        <div className="card" style={{ display: "flex", flexDirection: "column", gap: "1.5rem" }}>
            <div>
                <h2 style={{ fontSize: "1.1rem", fontWeight: 700, marginBottom: "0.25rem" }}>
                    🍕 Build Your Pizza
                </h2>
                <p style={{ fontSize: "0.85rem", color: "var(--text-muted)" }}>
                    Customize size and toppings, or pick a preset.
                </p>
            </div>

            <PresetSelector onSelect={handlePreset} />
            <hr className="divider" />
            <SizeSelector selected={size} onChange={setSize} />
            <ToppingSelector toppings={toppings} onChange={setToppings} />

            {error && (
                <p style={{ color: "var(--accent-red)", fontSize: "0.85rem" }}>⚠ {error}</p>
            )}

            <div style={{ display: "flex", alignItems: "center", justifyContent: "space-between" }}>
                {size && (
                    <span style={{ fontSize: "0.9rem", color: "var(--text-muted)" }}>
                        Estimated:{" "}
                        <strong style={{ color: "var(--accent-gold)" }}>${cost.toFixed(2)}</strong>
                    </span>
                )}
                <button
                    className="btn btn-primary"
                    style={{ marginLeft: "auto" }}
                    onClick={handleSubmit}
                    disabled={isSubmitting}
                >
                    {isSubmitting ? "Adding..." : "➕ Add Pizza to Order"}
                </button>
            </div>
        </div>
    );
}
