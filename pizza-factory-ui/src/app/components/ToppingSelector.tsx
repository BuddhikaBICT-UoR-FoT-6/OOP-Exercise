"use client";

import { useState } from "react";
import { ToppingRequest, ToppingType } from "@/lib/types";

const TOPPING_TYPES: ToppingType[] = ["Cheese", "Ham", "Pepperoni"];

const SUGGESTIONS: Record<ToppingType, string[]> = {
    Cheese: ["Mozzarella", "Cheddar", "Parmesan", "Gouda", "Brie"],
    Ham: ["Smoked Ham", "Honey Ham", "Prosciutto"],
    Pepperoni: ["Classic Pepperoni", "Spicy Pepperoni", "Turkey Pepperoni"],
};

interface Props {
    toppings: ToppingRequest[];
    onChange: (toppings: ToppingRequest[]) => void;
}

export default function ToppingSelector({ toppings, onChange }: Props) {
    const [activeType, setActiveType] = useState<ToppingType>("Cheese");
    const [customName, setCustomName] = useState("");

    const add = (name: string) => {
        if (!name.trim()) return;
        onChange([...toppings, { type: activeType, name: name.trim() }]);
        setCustomName("");
    };

    const remove = (index: number) => {
        onChange(toppings.filter((_, i) => i !== index));
    };

    const chipClass = (type: string) => {
        const t = type.toLowerCase();
        return `chip chip-${t}`;
    };

    return (
        <div>
            <span className="label">Add Toppings (+$2.00 each)</span>

            {/* Type tabs */}
            <div style={{ display: "flex", gap: "0.5rem", marginBottom: "0.75rem" }}>
                {TOPPING_TYPES.map((t) => (
                    <button
                        key={t}
                        className={`btn ${activeType === t ? "btn-primary" : "btn-ghost"}`}
                        style={{ padding: "0.4rem 1rem", fontSize: "0.85rem" }}
                        onClick={() => setActiveType(t)}
                    >
                        {t}
                    </button>
                ))}
            </div>

            {/* Suggestions */}
            <div style={{ display: "flex", flexWrap: "wrap", gap: "0.4rem", marginBottom: "0.75rem" }}>
                {SUGGESTIONS[activeType].map((name) => (
                    <button
                        key={name}
                        onClick={() => add(name)}
                        style={{
                            padding: "0.3rem 0.8rem",
                            borderRadius: "99px",
                            border: "1px solid var(--border)",
                            background: "transparent",
                            color: "var(--text-muted)",
                            fontSize: "0.8rem",
                            cursor: "pointer",
                            fontFamily: "inherit",
                            transition: "all 0.15s",
                        }}
                        onMouseEnter={(e) => {
                            (e.target as HTMLButtonElement).style.borderColor = "var(--accent-orange)";
                            (e.target as HTMLButtonElement).style.color = "var(--accent-orange)";
                        }}
                        onMouseLeave={(e) => {
                            (e.target as HTMLButtonElement).style.borderColor = "var(--border)";
                            (e.target as HTMLButtonElement).style.color = "var(--text-muted)";
                        }}
                    >
                        + {name}
                    </button>
                ))}
            </div>

            {/* Custom name input */}
            <div style={{ display: "flex", gap: "0.5rem", marginBottom: "1rem" }}>
                <input
                    className="input"
                    placeholder={`Custom ${activeType} name...`}
                    value={customName}
                    onChange={(e) => setCustomName(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && add(customName)}
                />
                <button className="btn btn-primary" onClick={() => add(customName)}>
                    Add
                </button>
            </div>

            {/* Added toppings chips */}
            {toppings.length > 0 && (
                <div style={{ display: "flex", flexWrap: "wrap", gap: "0.4rem" }}>
                    {toppings.map((t, i) => (
                        <span key={i} className={chipClass(t.type)}>
                            {t.name}
                            <button
                                onClick={() => remove(i)}
                                style={{
                                    background: "none",
                                    border: "none",
                                    cursor: "pointer",
                                    color: "inherit",
                                    padding: 0,
                                    fontSize: "0.85rem",
                                    lineHeight: 1,
                                }}
                            >
                                ✕
                            </button>
                        </span>
                    ))}
                </div>
            )}

            {toppings.length === 0 && (
                <p style={{ fontSize: "0.8rem", color: "var(--text-faint)" }}>
                    No toppings added yet. Pick from suggestions or type a custom name.
                </p>
            )}
        </div>
    );
}
