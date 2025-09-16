import { create } from "zustand";

interface useImageUploadModalStore {
  isOpen: boolean;
  onOpen: () => void;
  onClose: () => void;
}

export const useImageUploadModal = create<useImageUploadModalStore>((set) => ({
  isOpen: false,
  onOpen: () => set({ isOpen: true }),
  onClose: () => set({ isOpen: false }),
}));
