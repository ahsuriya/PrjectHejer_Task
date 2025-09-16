import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Customer } from "@/types/customer";
import { Mail, Phone, User } from "lucide-react";
import Link from "next/link";

export function CustomerCard({ customer }: { customer: Customer }) {
  return (
    <Card className="hover:shadow-xl transition-all duration-300 transform hover:-translate-y-1">
      <Link href={`/${customer.customerId}`} className="block">
        <CardHeader className="flex items-center mb-4">
          <div className="w-12 h-12 bg-blue-100 rounded-full flex items-center justify-center mr-4">
            <User className="w-6 h-6 text-blue-600" />
          </div>
          <CardTitle className="text-lg font-semibold text-gray-800 truncate">
            {customer.name}
          </CardTitle>
        </CardHeader>
        <CardContent className="space-y-3">
          <div className="flex items-center text-gray-600">
            <Phone className="w-4 h-4 mr-3 text-gray-400 flex-shrink-0" />
            <span className="text-sm">{customer.phone}</span>
          </div>
          <div className="flex items-center text-gray-600">
            <Mail className="w-4 h-4 mr-3 text-gray-400 flex-shrink-0" />
            <span className="text-sm truncate">{customer.email}</span>
          </div>
        </CardContent>
      </Link>
    </Card>
  );
}
