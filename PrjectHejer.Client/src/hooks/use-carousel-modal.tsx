import { create } from "zustand";

interface useCarouselModalStore {
  isOpen: boolean;
  onOpen: () => void;
  onClose: () => void;
}

export const useCarouselModal = create<useCarouselModalStore>((set) => ({
  isOpen: false,
  onOpen: () => set({ isOpen: true }),
  onClose: () => set({ isOpen: false }),
}));
