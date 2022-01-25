export const addToArray = (arraySetter: any, array: any[], object: any) => arraySetter([...array, object]);
export const setArray = (arraySetter: any, object: any) => arraySetter(object);
