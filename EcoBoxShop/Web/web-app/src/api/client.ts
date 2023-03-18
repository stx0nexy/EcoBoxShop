const handlerResponse = async (response: Response) => {
    if (!response.ok) {
        const message = await response.json();
        throw Error(message.error || 'Request error');
    };
    return response.json();
};

const apiClient = async ({ path, method, data }: apiClientPrors) => {
    const requestOptions = {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: !!data ? JSON.stringify(data) : undefined
    };
    return await fetch(`${path}`, requestOptions).then(handlerResponse);
};

interface apiClientPrors {
    path: string
    method: string
    data?: any
};

export default apiClient;