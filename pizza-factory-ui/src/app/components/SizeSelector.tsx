"use client";

import { PizzaSize } from "@/lib/types";

const SIZES: { size: PizzaSize; emoji: string; base: number; desc: string }[] = [
    { size: "Small", emoji: "🍕", base: 10, desc: "Perfect for 1" },
    { size: "Medium", emoji: "🍕🍕", base: 12, desc: "Great for 2" },
    { size: "Large", emoji: "🍕🍕🍕", base: 14, desc: "Feed the squad" },
];

interface Props {
    selected: PizzaSize | null;
    onChange: (size: PizzaSize) => void;
}

export default function SizeSelector({ selected, onChange }: Props) {
    return (
        <div>
            <span className="label">Choose Size</span>
            <div style={{ display: "grid", gridTemplateColumns: "repeat(3,1fr)", gap: "0.75rem" }}>
                {SIZES.map(({ size, emoji, base, desc }) => {
                    const isActive = selected === size;
                    return (
                        <button
                            key={size}
                            onClick={() => onChange(size)}
                            style={{
                                padding: "1rem 0.75rem",
                                borderRadius: "var(--radius-md)",
                                border: isActive
                                    ? "2px solid var(--accent-orange)"
                                    : "1px solid var(--border)",
                                background: isActive
                                    ? "rgba(255,107,53,0.1)"
                                    : "var(--bg-dark)",
                                cursor: "pointer",
                                textAlign: "center",
                                transition: "all 0.2s",
                                color: "var(--text-primary)",
                                fontFamily: "inherit",
                            }}
                        >
                            <div style={{ fontSize: "1.4rem", marginBottom: "0.3rem" }}>{emoji}</div>
                            <div style={{ fontWeight: 700, fontSize: "0.95rem" }}>{size}</div>
                            <div style={{ fontSize: "0.75rem", color: "var(--text-muted)" }}>{desc}</div>
                            <div
                                style={{
                                    marginTop: "0.4rem",
                                    fontSize: "0.85rem",
                                    fontWeight: 600,
                                    color: isActive ? "var(--accent-orange)" : "var(--text-muted)",
                                }}
                            >
                                ${base}.00 base
                            </div>
                        </button>
                    );
                })}
            </div>
        </div>
    );
}
