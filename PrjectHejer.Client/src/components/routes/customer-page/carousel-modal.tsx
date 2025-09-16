"use client";

import { Modal } from "@/components/shared/modal";
import { Card, CardContent } from "@/components/ui/card";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from "@/components/ui/carousel";
import { useCarouselModal } from "@/hooks/use-carousel-modal";
import { cn } from "@/lib/utils";
import { SingleCustomer } from "@/types/customer";
import { useParams, useRouter } from "next/navigation";
import { useState } from "react";

export const CarouselModal = ({
  images,
}: {
  images: SingleCustomer["Images"];
}) => {
  const [isLoading, setIsLoading] = useState(false);
  const router = useRouter();
  const params = useParams();

  const carouselModal = useCarouselModal();

  return (
    <Modal
      title="View Images"
      description="Browse through the uploaded images."
      isOpen={carouselModal.isOpen}
      onClose={carouselModal.onClose}
      className="!max-w-5xl !max-h-[90dvh] max-lg:!max-w-[95%]"
      scrollArea={false}
      contentClassName={cn("flex items-center justify-center h-fit")}
    >
      <Carousel className="w-full max-w-4xl max-lg:w-[90%]">
        <CarouselContent className="-ml-1">
          {images.map((image, index) => (
            <CarouselItem
              key={index}
              className="pl-1 md:basis-1/2 lg:basis-1/3"
            >
              <div className="p-1">
                <Card className="!aspect-square py-0">
                  <CardContent className="flex aspect-square items-center justify-center p-0 rounded-md overflow-hidden">
                    <img
                      src={`data:image/jpeg;base64,${image.base64Image}`}
                      alt={`Image ${image.id}`}
                      className="object-cover w-full h-full aspect-square"
                    />
                  </CardContent>
                </Card>
              </div>
            </CarouselItem>
          ))}
        </CarouselContent>
        <CarouselPrevious />
        <CarouselNext />
      </Carousel>
    </Modal>
  );
};
