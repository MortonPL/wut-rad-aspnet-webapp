// state-related functions
export const addToArray = (arraySetter: any, array: any[], object: any) => arraySetter([...array, object]);
export const setArray = (arraySetter: any, object: any) => arraySetter(object);
export const removeFromArray = (arraySetter: any, array: any[], object: any, key: string) => arraySetter(array.filter((other: any) => object[key] !== other[key]));

// generic functions
export const gremoveFromArray = (array: any[], object: any, key: string) => array.filter((other: any) => object[key] !== other[key]);
export const gmodifyInArray = (array: any[], object: any, key: string) => {
    const i = array.findIndex((other: any) => object[key] === other[key]);
    array[i] = object;
    return array;
}

export const dateToString = (date: Date) => {
    const d = date.getDate();
    const m = date.getMonth() + 1;
    const y = date.getFullYear();
    return [y, (m > 9 ? '' : '0') + m, (d > 9 ? '' : '0') + d].join('-');
};
