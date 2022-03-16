export class ApiResponse<T> {
  success: boolean;
  errorMessages: string[];
  referenceId: number;
  statusCode: number;
  content: T;
}
