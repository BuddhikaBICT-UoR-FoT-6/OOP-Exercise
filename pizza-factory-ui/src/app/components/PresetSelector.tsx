"use client";

import { useEffect, useState } from "react";
import { getPresets } from "@/lib/api";
import { Preset } from "@/lib/types";

interface Props {
    onSelect: (preset: Preset) => void;
}

export default function PresetSelector({ onSelect }: Props) {
    const [presets, setPresets] = useState<Preset[]>([]);

    useEffect(() => {
        getPresets().then(setPresets).catch(console.error);
    }, []);

    const PRESET_ICONS: Record<string, string> = {
        "Meat Lover": "🥩",
        "Margherita": "🍅",
        "Four Cheese": "🧀",
    };

    return (
        <div>
            <span className="label">⚡ Quick Presets</span>
            <div style={{ display: "flex", gap: "0.5rem", flexWrap: "wrap" }}>
                {presets.map((p) => (
                    <button
                        key={p.name}
                        className="btn btn-ghost"
                        style={{ fontSize: "0.85rem", padding: "0.4rem 1rem" }}
                        onClick={() => onSelect(p)}
                    >
                        {PRESET_ICONS[p.name] ?? "🍕"} {p.name}
                    </button>
                ))}
            </div>
        </div>
    );
}
