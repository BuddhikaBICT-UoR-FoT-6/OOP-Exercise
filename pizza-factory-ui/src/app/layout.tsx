import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "Pizza Factory",
  description: "Build your perfect pizza with our interactive pizza factory",
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body>
        <header className="site-header">
          <span className="logo">🍕 Pizza Factory</span>
          <span style={{ color: "var(--text-muted)", fontSize: "0.85rem" }}>
            OOP &amp; Builder Pattern Demo
          </span>
        </header>
        <main>{children}</main>
      </body>
    </html>
  );
}
