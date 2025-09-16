import { GalleryVerticalEnd } from "lucide-react";
import Link from "next/link";
import { Button } from "../ui/button";

export const Header = () => {
  return (
    <header className="h-16 px-4 w-full flex items-center justify-between">
      <Button
        asChild
        variant="ghost"
        size="icon"
        className="active:scale-95 transition-transform"
      >
        <Link href="/">
          <GalleryVerticalEnd />
        </Link>
      </Button>
    </header>
  );
};
