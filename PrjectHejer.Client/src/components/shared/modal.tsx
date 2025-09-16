"use client";

import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { ScrollArea } from "../ui/scroll-area";

interface ModalProps {
  title: string;
  description: string;
  isOpen: boolean;
  onClose: () => void;
  children?: React.ReactNode;
  className?: string;
  scrollArea?: boolean;
  contentClassName?: string;
}

export const Modal: React.FC<ModalProps> = ({
  title,
  description,
  isOpen,
  onClose,
  children,
  className,
  scrollArea = true,
  contentClassName,
}) => {
  const onChange = (open: boolean) => {
    if (isOpen) {
      onClose();
    }
  };

  return (
    <Dialog open={isOpen} onOpenChange={onChange}>
      <DialogContent className={className}>
        <DialogHeader className="h-full">
          <DialogTitle>{title}</DialogTitle>
          <DialogDescription>{description}</DialogDescription>
          {scrollArea && (
            <ScrollArea className="max-h-[calc(90dvh-48px-84px)] px-4">
              {children}
            </ScrollArea>
          )}
          {!scrollArea && <div className={contentClassName}>{children}</div>}
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
